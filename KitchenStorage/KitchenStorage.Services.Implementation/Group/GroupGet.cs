namespace KitchenStorage.Services.Implementation;

internal class GroupGet : IGroupGet
{
    private readonly IBaseQuery<Group> _groupQuery;

    public GroupGet(IBaseQuery<Group> groupQuery)
    {
        _groupQuery = groupQuery;
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _groupQuery.DisposeAsync();
    }

    public async Task<IEnumerable<Group>> GroupsAsync()
        => await _groupQuery.GetAllAsync();
}
