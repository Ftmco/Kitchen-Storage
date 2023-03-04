namespace KitchenStorage.Services.Implementation.MeasurmentType;

public class GetMeasurementType : IGetMeasurementType
{
    private readonly IBaseQuery<MeasurementType> _query;

    private readonly IBaseQuery<TypeConvert> _convertQuery;

    public GetMeasurementType(IBaseQuery<MeasurementType> query, IBaseQuery<TypeConvert> convertQuery)
    {
        _query = query;
        _convertQuery = convertQuery;
    }

    public async Task<IEnumerable<TypeConvert>> ConversionsAsync(Guid id)
            => await _convertQuery.GetAllAsync(tc => tc.FromTypeId == id);

    public async Task<PaginationResult<IEnumerable<MeasurementType>>> TypesAsync(int page, int count)
    {
        IEnumerable<MeasurementType> types = await _query.GetAllAsync(page, count);
        var typesCount = await _query.CountAsync();

        return types.GetPaginationResult(typesCount.PageCount(count));
    }
}
