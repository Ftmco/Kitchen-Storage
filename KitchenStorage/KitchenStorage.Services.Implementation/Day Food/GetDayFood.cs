namespace KitchenStorage.Services.Implementation;

internal class GetDayFood : IGetDayFood
{
    private readonly IBaseQuery<DayFood> _query;

    public GetDayFood(IBaseQuery<DayFood> query)
    {
        _query = query;
    }

    public async Task<PaginationResult<IEnumerable<DayFood>>> DayFoodsAsync(int page, int count)
    {
        IEnumerable<DayFood> foods = await _query.GetAllAsync(page, count);
        int foodsCount = await _query.CountAsync();

        return foods.GetPaginationResult(foodsCount.PageCount(count));
    }
}
