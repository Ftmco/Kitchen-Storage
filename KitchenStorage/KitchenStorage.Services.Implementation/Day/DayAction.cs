namespace KitchenStorage.Services.Implementation;

internal class DayAction : IDayAction
{
    private readonly IBaseCud<Day> _cud;

    private readonly IBaseQuery<Day> _query;

    public DayAction(IBaseCud<Day> cud, IBaseQuery<Day> query)
    {
        _cud = cud;
        _query = query;
    }

    public async Task<Either<DayActionStatus, Day>> CreateAsync(AddDayViewModel addDay)
    {
        if (await _query.AnyAsync(d => d.DayOfWeek == addDay.DayOfWeek || d.Name == addDay.Name))
            return DayActionStatus.Exist;

        Day day = new()
        {
            Name = addDay.Name,
            DayOfWeek = addDay.DayOfWeek,
        };

        return await _cud.InsertAsync(day) ?
                      day : DayActionStatus.Failed;
    }

    public async Task<DayActionStatus> DeleteAsync(Guid id)
        => await _cud.DeleteAsync(id)
                    ? DayActionStatus.Success
                    : DayActionStatus.Failed;
}
