namespace KitchenStorage.Services.Abstraction;

public interface IGetFoodHistory
{
    Task<PaginationResult<IEnumerable<FoodHistory>>> FoodHistoriesAsync(int page, int count);

}
