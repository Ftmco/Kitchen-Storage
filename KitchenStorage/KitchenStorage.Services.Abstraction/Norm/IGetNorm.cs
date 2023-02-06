namespace KitchenStorage.Services.Abstraction
{
    public interface IGetNorm
    {
        Task<IEnumerable<Norm>> NormsAsync(Guid FoodId);
        Task<Norm?> FindByIdAsync(Guid id);
    }
}
