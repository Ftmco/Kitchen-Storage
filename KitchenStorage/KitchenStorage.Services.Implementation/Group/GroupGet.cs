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

    public async Task<PaginationResult<IEnumerable<Group>>> GroupsAsync(int page, int count)
    {
        var groups = await _groupQuery.GetAllAsync(page, count);
        var groupsCount = await _groupQuery.CountAsync();

        return groups.GetPaginationResult(groupsCount.PageCount(count));
    }
}
