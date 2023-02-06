namespace KitchenStorage.Services.Abstraction
{
    public interface INormAction
    {
        Task<Either<FoodActionStatus, Norm>> CreateAsync(NormViewModel norm);

        Task<Either<FoodActionStatus, Norm>> DeleteAsync(Guid id);
    }
}
