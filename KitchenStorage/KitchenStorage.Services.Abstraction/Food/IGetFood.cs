namespace KitchenStorage.Services.Abstraction
{
    public interface IGetFood
    {
        Task<IEnumerable<Food>> FoodsAsync();
        Task<Food?> FindByIdAsync(Guid id);
    }
}
