namespace KitchenStorage.Services.Abstraction;

public interface IInventoryViewModel
{
    Task<InventoryViewModel> CreateInventoryViewModelAsync(Inventory inventory);

    Task<IEnumerable<InventoryViewModel>> CreateInventoryViewModelAsync(IEnumerable<Inventory> inventories);
}
