namespace KitchenStorage.Services.Abstraction
{
    public interface IGetInventory 
    {
        Task<IEnumerable<Inventory>> InventorysAsync();

        Task<Inventory?> FindByIdAsync(Guid id);
    }
}
