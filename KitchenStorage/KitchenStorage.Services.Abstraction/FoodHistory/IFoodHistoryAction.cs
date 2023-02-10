namespace KitchenStorage.Services.Abstraction
{
    public interface IFoodHistoryAction
    {
        Task<Either<FoodHistoryActionStatus, FoodHistory>> UpsertAsync(UpsertFoodHistoryViewModel upsert);

        Task<Either<FoodHistoryActionStatus, FoodHistory>> CreateAsync(UpsertFoodHistoryViewModel upsert);

        Task<Either<FoodHistoryActionStatus, FoodHistory>> UpdateAsync(UpsertFoodHistoryViewModel upsert);

        Task<FoodHistoryActionStatus> DeleteAsync(Guid id);

    }
}
