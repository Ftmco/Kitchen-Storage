using System.Linq.Expressions;

namespace KitchenStorage.Services.Implementation;

public class GetInventory : IGetInventory
{
    private readonly IBaseQuery<Inventory> _inventoryQuery;

    public GetInventory(IBaseQuery<Inventory> inventoryQuery)
    {
        _inventoryQuery = inventoryQuery;
    }

    public async Task<PaginationResult<IEnumerable<Inventory>>> InventorysAsync(int page, int count, string? q)
    {
        Expression<Func<Inventory, bool>> query = n => q == null || n.Name.Contains(q);
        IEnumerable<Inventory> inventory = await _inventoryQuery.GetAllAsync(page, count);
        var inventoryCount = await _inventoryQuery.CountAsync(query);

        return inventory.GetPaginationResult(inventoryCount.PageCount(count));
    }
    public async Task<PaginationResult<IEnumerable<Inventory>>> GetAlertLimitAsync(int page, int count)
    {
        IEnumerable<Inventory> inventory = await _inventoryQuery
            .GetAllAsync(x => x.Value <= x.AlertLimit, page, count);
        var inventoryCount = await _inventoryQuery.CountAsync(x => x.Value <= x.AlertLimit);

        return inventory.GetPaginationResult(inventoryCount.PageCount(count));
    }


    public async Task<IEnumerable<Inventory>> InventorysAsync()
        => await _inventoryQuery.GetAllAsync();
}
