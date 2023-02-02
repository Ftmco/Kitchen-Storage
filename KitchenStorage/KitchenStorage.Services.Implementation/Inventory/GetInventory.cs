namespace KitchenStorage.Services.Implementation
{
    public class GetInventory : IGetInventory
    {
        private readonly IBaseQuery<Inventory> _inventoryQuery;

        public GetInventory(IBaseQuery<Inventory> inventoryQuery)
        {
            _inventoryQuery = inventoryQuery;
        }

        public async Task<IEnumerable<Inventory>> InventorysAsync()
        {
            return await _inventoryQuery.GetAllAsync();
        }

        public async Task<Inventory?> FindByIdAsync(Guid id)
        {
            return await _inventoryQuery.GetAsync(id);
        }
    }
}
