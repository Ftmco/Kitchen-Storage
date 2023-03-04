namespace KitchenStorage.Services.Implementation;

public class GetHistory : IGetHistory
{
    private readonly IBaseQuery<FoodHistory> _query;

    private readonly IBaseQuery<InventoryHistory> _invetoryHistoryQuery;

    public GetHistory(IBaseQuery<FoodHistory> query, IBaseQuery<InventoryHistory> invetoryHistoryQuery)
    {
        _query = query;
        _invetoryHistoryQuery = invetoryHistoryQuery;
    }

    public async Task<PaginationResult<IEnumerable<FoodHistory>>> FoodHistoriesAsync(int page, int count)
    {

        IEnumerable<FoodHistory> foodHistories = await _query.GetAllAsync(page, count, x => x.CreateDate);
        var foodHistoriesCount = await _query.CountAsync();

        return foodHistories.GetPaginationResult(foodHistoriesCount.PageCount(count));
    }

    public async Task<PaginationResult<IEnumerable<InventoryHistory>>> InventoryHistoriesAsync(int page, int count)
    {
        IEnumerable<InventoryHistory> histories = await _invetoryHistoryQuery.GetAllAsync(page, count);
        var historiesCount = await _invetoryHistoryQuery.CountAsync();

        return histories.GetPaginationResult(historiesCount.PageCount(count));
    }
}
