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

    public DayFoodAction(IBaseQuery<DayFood> query, IBaseCud<DayFood> cud,
        IBaseQuery<Food> foodQuery, IGetNorm normGet,
        IBaseQuery<Inventory> inventoryQeury, IBaseQuery<MeasurementType> typeQuery,
        IBaseQuery<TypeConvert> convertQuery, IBaseCud<Inventory> inventoryCud, IBaseCud<FoodHistory> historyCud)
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
        foreach (Norm norm in norms)
        {
            Inventory? inventory = await _inventoryQuery.GetAsync(norm.InventoryId);
            if (inventory is null)
                return DayFoodActionStatus.LackOfInventory;

            double value = await MealValueAsync(inventory, norm, makeMeal.Count);
            if (inventory.Value < value)
                return DayFoodActionStatus.LackOfInventory;

            inventory.Value -= value;
            await _inventoryCud.UpdateAsync(inventory);
        }

        await _historyCud.InsertAsync(new FoodHistory
        {
            DayId = dayFood.DayId,
            FoodId = dayFood.FoodId,
            Count = makeMeal.Count,
            Description = makeMeal.Description,
            Meal = dayFood.Meal,
        });

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
}
