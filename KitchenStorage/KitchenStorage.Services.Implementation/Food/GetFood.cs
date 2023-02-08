namespace KitchenStorage.Services.Implementation
{
    public class GetFood : IGetFood
    {
        private readonly IBaseQuery<Food> _query;

        public GetFood(IBaseQuery<Food> query)
        {
            _query = query;
        }

        public async Task<PaginationResult<IEnumerable<Food>>> FoodsAsync(int page, int count)
        {
            IEnumerable<Food> food = await _query.GetAllAsync(page, count);
            var inventoryCount = await _query.CountAsync();

            return food.GetPaginationResult(inventoryCount.PageCount(count));
        }
    }
}
