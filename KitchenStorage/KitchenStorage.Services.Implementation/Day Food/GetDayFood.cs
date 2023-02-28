using System.Linq.Expressions;

namespace KitchenStorage.Services.Implementation;

internal class GetDayFood : IGetDayFood
{
    private readonly IBaseQuery<DayFood> _query;

    public GetDayFood(IBaseQuery<DayFood> query)
    {
        _query = query;
    }

    public async Task<PaginationResult<IEnumerable<DayFood>>> DayFoodsAsync(int page, int count, string? q)
    {
        Expression<Func<DayFood, bool>> query = df => q == null || df.Meal.Contains(q);
        IEnumerable<DayFood> foods = await _query.GetAllAsync(query, page, count);
        int foodsCount = await _query.CountAsync(query);

        return foods.GetPaginationResult(foodsCount.PageCount(count));
    }
}
