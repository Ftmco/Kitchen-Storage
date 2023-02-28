using System.Linq.Expressions;

namespace KitchenStorage.Services.Implementation
{
    public class GetFood : IGetFood
    {
        private readonly IBaseQuery<Food> _query;

        public GetFood(IBaseQuery<Food> query)
        {
            _query = query;
        }

        public async Task<PaginationResult<IEnumerable<Food>>> FoodsAsync(int page, int count, string? q)
        {
            Expression<Func<Food, bool>> query = f => q == null || f.Name.Contains(q);
            IEnumerable<Food> food = await _query.GetAllAsync(query, page, count);
            var inventoryCount = await _query.CountAsync(query);

            return food.GetPaginationResult(inventoryCount.PageCount(count));
        }
    }
}
