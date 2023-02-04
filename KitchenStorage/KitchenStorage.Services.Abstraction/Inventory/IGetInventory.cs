namespace KitchenStorage.Services.Abstraction;

public interface IGetInventory
{
    Task<PaginationResult<IEnumerable<Inventory>>> InventorysAsync(int page, int count);

}
