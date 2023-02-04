namespace KitchenStorage.Services.Implementation
{
    public class GetFood : IGetFood
    {
        private readonly IBaseQuery<Food> _query;

        public GetFood(IBaseQuery<Food> query)
        {
            _query = query;
        }

        public async Task<Food?> FindByIdAsync(Guid id)
            => await _query.GetAsync(id);

        public async Task<IEnumerable<Food>> FoodsAsync()
            => await _query.GetAllAsync();
    }
}
