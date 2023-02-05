using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace KitchenStorage.Services.Implementation;

internal class GroupAction : IGroupAction
{
    private readonly IBaseCud<Group> _groupCud;

    private readonly IBaseQuery<Group> _groupQuery;

    public GroupAction(IBaseCud<Group> groupCud, IBaseQuery<Group> groupQuery)
    {
        _groupCud = groupCud;
        _groupQuery = groupQuery;
    }

    public async Task<Either<GroupActionStatus, Group>> CreateAsync(UpsertGroupViewModel upsert)
    {
        Group newGroup = new()
        {
            Name = upsert.Name,
            Status = upsert.Status,
        };

        return await _groupCud.InsertAsync(newGroup) ?
                    newGroup : GroupActionStatus.Failed;
    }

    public async Task<Either<GroupActionStatus, Group>> DeleteAsync(Guid id)
    {
        Group? group = await _groupQuery.GetAsync(id);
        if (group is null)
            return GroupActionStatus.NotFound;

        group.Status = (byte)EntityState.Deleted;
        return await _groupCud.UpdateAsync(group) ?
                    group : GroupActionStatus.Failed;
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _groupCud.DisposeAsync();
        await _groupQuery.DisposeAsync();
    }

    public async Task<Either<GroupActionStatus, Group>> UpdateAsync(UpsertGroupViewModel upsert)
    {
        Group? group = await _groupQuery.GetAsync(upsert.Id);
        if (group is null)
            return GroupActionStatus.NotFound;

        group.Name = upsert.Name;
        group.Status = upsert.Status;
        return await _groupCud.UpdateAsync(group) ?
                    group : GroupActionStatus.Failed;
    }

    public async Task<Either<GroupActionStatus, Group>> UpsertAsync(UpsertGroupViewModel upsert)
        => upsert.Id is null
                ? await CreateAsync(upsert)
                : await UpdateAsync(upsert);
}
