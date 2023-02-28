namespace KitchenStorage.Services.Abstraction;

public interface IGetHistory
{
    Task<PaginationResult<IEnumerable<FoodHistory>>> FoodHistoriesAsync(int page, int count, string? q);

    Task<PaginationResult<IEnumerable<InventoryHistory>>> InventoryHistoriesAsync(int page, int count, string? q);
}
