namespace KitchenStorage.Services.Abstraction;

public interface IGetFood
{
    Task<PaginationResult<IEnumerable<Food>>> FoodsAsync(int page, int count);

}
