namespace KitchenStorage.ViewModel
{
    public record NormViewModel
        (Guid? Id, double Value, Guid FoodId, Guid InventoryId, byte Status, string CreateDate);

}
