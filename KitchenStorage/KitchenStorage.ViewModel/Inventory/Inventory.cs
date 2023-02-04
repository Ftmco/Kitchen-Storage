namespace KitchenStorage.ViewModel
{

    public record InventoryViewModel
        (Guid? Id, string Name, double Value, int TypeId, string Description, byte Status, string CreateDate);

    public record UpsertInventoryViewModel
        (Guid? Id, string Name, double Value, int TypeId, string Description);

    public enum InventoryActionStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2
    }
    public enum InventoryType
    {
        LogIn = 0,
        LogOut = 1
    }
}
