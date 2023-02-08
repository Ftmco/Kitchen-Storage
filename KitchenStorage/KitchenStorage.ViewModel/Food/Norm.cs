namespace KitchenStorage.ViewModel;

public record NormViewModel(Guid? Id, double Value, Guid FoodId, byte Status, string CreateDate, InventoryPreviewViewModel? Inventory);

public record AddNormViewModel(double Value, Guid FoodId, Guid InventoryId, Guid TypeId);

