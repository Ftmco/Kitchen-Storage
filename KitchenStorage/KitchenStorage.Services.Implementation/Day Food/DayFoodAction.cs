namespace KitchenStorage.Services.Implementation;

internal class DayFoodAction : IDayFoodAction
{
    private readonly IBaseQuery<DayFood> _query;

    private readonly IBaseCud<DayFood> _cud;

    public Task<Either<DayFoodActionStatus, DayFood>> CreateAsync(UpsertDayFoodViewModel create)
    {
        throw new NotImplementedException();
    }

    public Task<Either<DayFoodActionStatus, DayFood>> UpdateAsync(UpsertDayFoodViewModel update)
    {
        throw new NotImplementedException();
    }

    public Task<Either<DayFoodActionStatus, DayFood>> UpsertAsync(UpsertDayFoodViewModel upsert)
    {
        throw new NotImplementedException();
    }
}
