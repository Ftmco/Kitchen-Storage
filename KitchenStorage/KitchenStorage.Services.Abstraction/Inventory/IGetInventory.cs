namespace KitchenStorage.Services.Abstraction
{
    public interface IGetInventory
    {
        Task<IEnumerable<Inventory>> InventorysAsync();

}
