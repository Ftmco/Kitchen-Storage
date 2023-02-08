namespace KitchenStorage.Services.Implementation;

internal class DayFoodAction : IDayFoodAction
{
    private readonly IBaseQuery<DayFood> _query;

    private readonly IBaseCud<DayFood> _cud;

    public DayFoodAction(IBaseQuery<DayFood> query, IBaseCud<DayFood> cud)
    {
        _query = query;
        _cud = cud;
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
}
