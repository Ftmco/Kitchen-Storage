namespace KitchenStorage.Services.Implementation
{
    public class GetNorm : IGetNorm
    {
        private readonly IBaseQuery<Norm> _query;

        public GetNorm(IBaseQuery<Norm> query)
        {
            _query = query;
        }

        public async Task<Norm?> FindByIdAsync(Guid id)
            => await _query.GetAsync(id);

        public async Task<IEnumerable<Norm>> NormsAsync()
            => await _query.GetAllAsync();
    }
}
