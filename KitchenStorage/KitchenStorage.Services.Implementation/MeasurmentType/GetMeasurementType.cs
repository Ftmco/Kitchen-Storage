namespace KitchenStorage.Services.Implementation.MeasurmentType
{
    public class GetMeasurementType : IGetMeasurementType
    {
        private readonly IBaseQuery<MeasurementType> _query;

        public GetMeasurementType(IBaseQuery<MeasurementType> query)
        {
            _query = query;
        }

        public async Task<PaginationResult<IEnumerable<MeasurementType>>> TypesAsync(int page, int count)
        {
            IEnumerable<MeasurementType> types = await _query.GetAllAsync(page, count);
            var typesCount = await _query.CountAsync();

            return types.GetPaginationResult(typesCount.PageCount(count));
        }
    }
}
