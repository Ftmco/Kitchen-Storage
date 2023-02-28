namespace KitchenStorage.Services.Abstraction;

public interface IGetInventory
{
    Task<IEnumerable<Inventory>> InventorysAsync();

    Task<PaginationResult<IEnumerable<Inventory>>> InventorysAsync(int page, int count, string? q);

    Task<PaginationResult<IEnumerable<Inventory>>> GetAlertLimitAsync(int page, int count);
}
