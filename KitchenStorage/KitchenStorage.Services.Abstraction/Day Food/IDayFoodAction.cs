namespace KitchenStorage.Services.Abstraction;

public interface IDayFoodAction
{
    Task<Either<DayFoodActionStatus, DayFood>> UpsertAsync(UpsertDayFoodViewModel upsert);

    Task<Either<DayFoodActionStatus, DayFood>> UpdateAsync(UpsertDayFoodViewModel update);

    Task<Either<DayFoodActionStatus, DayFood>> CreateAsync(UpsertDayFoodViewModel create);
}
