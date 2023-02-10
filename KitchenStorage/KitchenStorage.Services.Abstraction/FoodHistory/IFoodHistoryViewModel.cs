namespace KitchenStorage.Services.Abstraction
{
    public interface IFoodHistoryViewModel
    {
        Task<FoodHistoryViewModel> CreateFoodHistoryViewModel(FoodHistory foodHistory);

        Task<IEnumerable<FoodHistoryViewModel>> CreateFoodHistoryViewModel(IEnumerable<FoodHistory> foodHistories);
    }
}
