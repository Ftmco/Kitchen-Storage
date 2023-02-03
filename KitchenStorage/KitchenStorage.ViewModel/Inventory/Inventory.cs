namespace KitchenStorage.ViewModel
{

    public record InventoryViewModel
        (Guid? Id, string Name, double Value, byte Type, string Description, string CreateDate);

    public record UpsertInventoryViewModel
        (Guid? Id, string Name, double Value, byte Type, string Description);

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
