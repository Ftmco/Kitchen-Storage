namespace KitchenStorage.Services.Abstraction
{
    public interface IGetNorm
    {
        Task<IEnumerable<Norm>> NormsAsync();
        Task<Norm?> FindByIdAsync(Guid id);
    }
}
