namespace KitchenStorage.Services.Implementation
{
    public class GetFoodHistory : IGetFoodHistory
    {
        private readonly IBaseQuery<FoodHistory> _query;

        public GetFoodHistory(IBaseQuery<FoodHistory> query)
        {
            _query = query;
        }

        public async Task<PaginationResult<IEnumerable<FoodHistory>>> FoodHistoriesAsync(int page, int count)
        {
            IEnumerable<FoodHistory> foodHistories = await _query.GetAllAsync(page, count, x => x.CreateDate);
            var foodHistoriesCount = await _query.CountAsync();

            return foodHistories.GetPaginationResult(foodHistoriesCount.PageCount(count));
        }
        public async Task<IEnumerable<FoodHistory>> FoodHistoriesAsync()
            => await _query.GetAllAsync();
    }
}
