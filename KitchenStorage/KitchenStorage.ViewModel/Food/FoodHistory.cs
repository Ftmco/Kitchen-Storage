namespace KitchenStorage.ViewModel;

public record FoodHistoryViewModel
(Guid Id, DayViewModel? Day, FoodViewModel? Food, int Count, string Meal, string Description, string CreateDate);

public record InventoryHistoryViewModel(Guid Id, string Description, string CreateDate, byte Type, IEnumerable<InventoryHistoryListViewModel> HistoryList);

public record InventoryHistoryListViewModel(Guid Id, Guid HistoryId, double Value, double CurrentValue, double NextValue, InventoryPreviewViewModel? Inventory);

public enum FoodHistoryActionStatus
{
    Success = 0,
    Failed = 1,
    NotFound = 2
}
