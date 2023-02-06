namespace KitchenStorage.ViewModel
{
    public record NormViewModel
        (Guid? Id, string Name, double Value, Guid FoodId, Guid InventoryId, byte Status, string CreateDate);

}
