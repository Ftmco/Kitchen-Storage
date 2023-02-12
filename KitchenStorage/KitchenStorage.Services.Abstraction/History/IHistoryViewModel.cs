namespace KitchenStorage.Services.Abstraction;

public interface IHistoryViewModel
{
    Task<FoodHistoryViewModel> CreateFoodHistoryViewModel(FoodHistory foodHistory);

    Task<IEnumerable<FoodHistoryViewModel>> CreateFoodHistoryViewModel(IEnumerable<FoodHistory> foodHistories);

    Task<InventoryHistoryViewModel> CreateInventoryHisstoryViewModelAsync(InventoryHistory history);

    Task<IEnumerable<InventoryHistoryViewModel>> CreateInventoryHisstoryViewModelAsync(IEnumerable<InventoryHistory> history);

    Task<IEnumerable<InventoryHistoryListViewModel>> CreateInventoryHistoryItemViewModel(IEnumerable<InventoryHistoryList> inventoryHistories);

    Task<InventoryHistoryListViewModel> CreateInventoryHistoryItemViewModel(InventoryHistoryList inventoryHistory);
}
