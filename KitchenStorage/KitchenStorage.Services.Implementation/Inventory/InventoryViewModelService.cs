namespace KitchenStorage.Services.Implementation
{
    public class InventoryViewModelService : IInventoryViewModel
    {
        public InventoryViewModel CreateInventoryViewModel(Inventory inventory)
             => new(Id: inventory.Id,
                 Name: inventory.Name,
                 Value: inventory.Value,
                 AlertLimit: inventory.AlertLimit,
                 Type: null,
                 Description: inventory.Description,
                 Status: inventory.Status,
                 CreateDate: inventory.CreateDate.ToShamsi());

        public IEnumerable<InventoryViewModel> CreateInventoryViewModel(IEnumerable<Inventory> inventories)
            => inventories.Select(CreateInventoryViewModel);
    }
}
