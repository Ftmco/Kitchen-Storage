namespace KitchenStorage.Services.Abstraction;

public interface IGetHistory
{
    Task<PaginationResult<IEnumerable<FoodHistory>>> FoodHistoriesAsync(int page, int count);

    Task<PaginationResult<IEnumerable<InventoryHistory>>> InventoryHistoriesAsync(int page, int count);
}
