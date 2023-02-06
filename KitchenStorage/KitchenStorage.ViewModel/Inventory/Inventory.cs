namespace KitchenStorage.ViewModel;

public record InventoryViewModel
    (Guid? Id, string Name, double Value, double AlertLimit, string Description, string CreateDate, double Status, GroupViewModel? Group, MeasurementTypeViewModel? Type);

public record InventoryPreviewViewModel(Guid Id, string Name);

public record UpsertInventoryViewModel
    (Guid? Id, string Name, double Value, double AlertLimit, Guid GroupId, Guid TypeId, string Description);

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
