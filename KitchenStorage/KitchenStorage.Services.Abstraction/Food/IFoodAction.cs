namespace KitchenStorage.Services.Abstraction
{
    public interface IFoodAction
    {
        Task<Either<FoodActionStatus, Food>> UpsertAsync(UpsertFoodViewModel upsert);

        Task<Either<FoodActionStatus, Food>> CreateAsync(UpsertFoodViewModel upsert);

        Task<Either<FoodActionStatus, Food>> UpdateAsync(UpsertFoodViewModel upsert);

        Task<Either<FoodActionStatus, Food>> DeleteAsync(Guid id);

    }
}
