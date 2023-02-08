namespace KitchenStorage.Services.Abstraction;

public interface IInventoryViewModel
{
    Task<InventoryViewModel> CreateInventoryViewModelAsync(Inventory inventory);

    Task<IEnumerable<InventoryViewModel>> CreateInventoryViewModelAsync(IEnumerable<Inventory> inventories);

    InventoryPreviewViewModel CreatePreviewInventiryViewModel(Inventory inventory);

    IEnumerable<InventoryPreviewViewModel> CreatePreviewInventiryViewModel(IEnumerable<Inventory> inventory);
}
