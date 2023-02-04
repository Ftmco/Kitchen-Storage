namespace KitchenStorage.Services.Abstraction
{
    public interface IInventoryViewModel
    {
        InventoryViewModel CreateInventoryViewModel(Inventory inventory);

        IEnumerable<InventoryViewModel> CreateInventoryViewModel(IEnumerable<Inventory> inventories);
    }
}
