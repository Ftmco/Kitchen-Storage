namespace KitchenStorage.Services.Implementation;

internal class GetDay : IGetDay
{
    private readonly IBaseQuery<Day> _query;

    public GetDay(IBaseQuery<Day> query)
    {
        _query = query;
    }

    public async Task<IEnumerable<Day>> DaysAsync()
            => await _query.GetAllAsync();
}
