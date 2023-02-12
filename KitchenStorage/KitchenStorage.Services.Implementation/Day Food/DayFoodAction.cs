namespace KitchenStorage.Services.Implementation;

internal class DayFoodAction : IDayFoodAction
{
    private readonly IBaseQuery<DayFood> _query;

    private readonly IBaseCud<DayFood> _cud;

    private readonly IBaseQuery<Food> _foodQuery;

    private readonly IBaseQuery<Inventory> _inventoryQuery;

    private readonly IBaseQuery<MeasurementType> _typeQuery;

    private readonly IGetNorm _normGet;

    private readonly IBaseQuery<TypeConvert> _convertQuery;

    private readonly IBaseCud<Inventory> _inventoryCud;

    private readonly IBaseCud<FoodHistory> _historyCud;

    private readonly IBaseCud<InventoryHistory> _inventoryHistory;

    private readonly IBaseQuery<InventoryHistory> _inventoryHistoryQuery;

    private readonly IBaseCud<InventoryHistoryList> _inventoryHistoryList;

    public DayFoodAction(IBaseQuery<DayFood> query, IBaseCud<DayFood> cud,
        IBaseQuery<Food> foodQuery, IGetNorm normGet,
        IBaseQuery<Inventory> inventoryQeury, IBaseQuery<MeasurementType> typeQuery,
        IBaseQuery<TypeConvert> convertQuery, IBaseCud<Inventory> inventoryCud,
        IBaseCud<FoodHistory> historyCud, IBaseCud<InventoryHistory> inventoryHistory,
        IBaseCud<InventoryHistoryList> inventoryHistoryList, IBaseQuery<InventoryHistory> inventoryHistoryQuery)
    {
        _query = query;
        _cud = cud;
        _foodQuery = foodQuery;
        _normGet = normGet;
        _inventoryQuery = inventoryQeury;
        _typeQuery = typeQuery;
        _convertQuery = convertQuery;
        _inventoryCud = inventoryCud;
        _historyCud = historyCud;
        _inventoryHistory = inventoryHistory;
        _inventoryHistoryList = inventoryHistoryList;
        _inventoryHistoryQuery = inventoryHistoryQuery;
    }

    public async Task<Either<DayFoodActionStatus, DayFood>> CreateAsync(UpsertDayFoodViewModel create)
    {
        DayFood dayFood = new()
        {
            DayId = create.DayId,
            FoodId = create.FoodId,
            Meal = create.Meal,

        };

        return await _cud.InsertAsync(dayFood) ?
                        dayFood : DayFoodActionStatus.Failed;
    }

    public async Task<DayFoodActionStatus> DeleteAsync(Guid id)
        => await _cud.DeleteAsync(id) ?
                        DayFoodActionStatus.Success
                        : DayFoodActionStatus.Failed;

    public async Task<DayFoodActionStatus> MakeMealAsync(MakeMealViewModel makeMeal)
    {
        DayFood? dayFood = await _query.GetAsync(makeMeal.Id);
        if (dayFood is null)
            return DayFoodActionStatus.NotFound;

        Food? food = await _foodQuery.GetAsync(dayFood.FoodId);
        if (food is null)
            return DayFoodActionStatus.NotFound;

        IEnumerable<Norm> norms = await _normGet.NormsAsync(food.Id);
        var inventories = new List<Inventory>();
        List<InventoryHistory> logsOfInventory = new();

        Guid logId = Guid.NewGuid();

        foreach (Norm norm in norms)
        {
            Inventory? inventory = await _inventoryQuery.GetAsync(norm.InventoryId);

            if (inventory is null)
                return DayFoodActionStatus.LackOfInventory;

            double value = await MealValueAsync(inventory, norm, makeMeal.Count);
            if (inventory.Value < value)
            {
                logsOfInventory.ForEach(async (log) =>
                {
                    await _inventoryHistoryList.DeleteAsync(l => l.HistoryId == log.Id);
                    await _inventoryHistory.DeleteAsync(log);
                });
                return DayFoodActionStatus.LackOfInventory;
            }

            inventory.Value -= value;
            inventories.Add(inventory);
            await LogInventoryHistory(logId, inventory, makeMeal, value);
        }
        await _inventoryCud.UpdateAsync(inventories);

        await LogFoodHistory(dayFood, makeMeal);

        return DayFoodActionStatus.Success;
    }

    public async Task<Either<DayFoodActionStatus, DayFood>> UpdateAsync(UpsertDayFoodViewModel update)
    {
        DayFood? dayFood = await _query.GetAsync(update.Id);
        if (dayFood is null)
            return DayFoodActionStatus.NotFound;

        dayFood.FoodId = update.FoodId;
        dayFood.DayId = update.DayId;
        dayFood.Meal = update.Meal;

        return await _cud.UpdateAsync(dayFood) ?
                     dayFood : DayFoodActionStatus.Failed;
    }

    public async Task<Either<DayFoodActionStatus, DayFood>> UpsertAsync(UpsertDayFoodViewModel upsert)
            => upsert.Id is null
                            ? await CreateAsync(upsert)
                            : await UpdateAsync(upsert);

    async Task<double> MealValueAsync(Inventory inventory, Norm norm, int count)
    {
        MeasurementType? normType = await _typeQuery.GetAsync(norm.TypeId);
        MeasurementType? inventoryType = await _typeQuery.GetAsync(inventory.TypeId);

        TypeConvert? conversion = await _convertQuery.GetAsync(c => c.FromTypeId == inventoryType!.Id && c.ToTypeId == normType!.Id);
        double value = norm.Value * count;

        if (conversion is not null)
            value = ((conversion.FromValue * norm.Value) / conversion.ToValue) * count;

        return value;
    }

    async Task LogFoodHistory(DayFood dayFood, MakeMealViewModel makeMeal)
     => await _historyCud.InsertAsync(new FoodHistory
     {
         DayId = dayFood.DayId,
         FoodId = dayFood.FoodId,
         Count = makeMeal.Count,
         Description = makeMeal.Description,
         Meal = dayFood.Meal,
     });

    async Task<InventoryHistory> LogInventoryHistory(Guid generateId, Inventory inventory, MakeMealViewModel makeMeal, double value)
    {
        InventoryHistory? history = await _inventoryHistoryQuery.GetAsync(h => h.GenerateId == generateId);
        if (history is null)
        {
            history = new()
            {
                GenerateId = generateId,
                Description = makeMeal.Description,
                Type = (byte)InventoryHistoryType.Output,
            };
            await _inventoryHistory.InsertAsync(history);
        }

        InventoryHistoryList item = new()
        {
            HistoryId = history.Id,
            InventoryId = inventory.Id,
            InventoryValue = inventory.Value,
            Value = value,
        };

        await _inventoryHistoryList.InsertAsync(item);

        return history;
    }
}
