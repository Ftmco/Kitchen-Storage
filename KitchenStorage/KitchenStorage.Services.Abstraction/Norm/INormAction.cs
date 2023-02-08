namespace KitchenStorage.Services.Abstraction
{
    public interface INormAction
    {
        Task<Either<FoodActionStatus, Norm>> CreateAsync(AddNormViewModel norm);

        Task<FoodActionStatus> DeleteAsync(Guid id);
    }
}
