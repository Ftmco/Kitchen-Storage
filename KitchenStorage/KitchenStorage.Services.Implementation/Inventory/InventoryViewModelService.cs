namespace KitchenStorage.Services.Implementation
{
    public class InventoryViewModelService : IInventoryViewModel
    {
        public InventoryViewModel CreateInventoryViewModel(Inventory inventory)
             => new(Id: inventory.Id,
                 Name: inventory.Name,
                 Value: inventory.Value,
                 TypeId: inventory.TypeId,
                 Description: inventory.Description,
                 CreateDate: inventory.CreateDate.ToShamsi());

        public IEnumerable<InventoryViewModel> CreateInventoryViewModel(IEnumerable<Inventory> inventories)
            => inventories.Select(CreateInventoryViewModel);
    }
}
