using System.Linq.Expressions;

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

    public async Task<PaginationResult<IEnumerable<Group>>> GroupsAsync(int page, int count, string? q)
    {
        Expression<Func<Group, bool>> query = g => q == null || g.Name.Contains(q);
        IEnumerable<Group> groups = await _groupQuery.GetAllAsync(query, page, count);
        var groupsCount = await _groupQuery.CountAsync(query);

        return groups.GetPaginationResult(groupsCount.PageCount(count));
    }
}
