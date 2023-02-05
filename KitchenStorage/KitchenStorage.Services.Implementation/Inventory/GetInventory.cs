namespace KitchenStorage.Services.Implementation;

public class GetInventory : IGetInventory
{
    private readonly IBaseQuery<Inventory> _inventoryQuery;

    public GetInventory(IBaseQuery<Inventory> inventoryQuery)
    {
        _inventoryQuery = inventoryQuery;
    }

    public async Task<PaginationResult<IEnumerable<Inventory>>> InventorysAsync(int page, int count)
    {
        IEnumerable<Inventory> inventory = await _inventoryQuery.GetAllAsync(page, count);
        var inventoryCount = await _inventoryQuery.CountAsync();

        return inventory.GetPaginationResult(inventoryCount.PageCount(count));
    }
}
