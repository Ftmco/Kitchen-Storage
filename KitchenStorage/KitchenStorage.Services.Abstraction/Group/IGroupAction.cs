namespace KitchenStorage.Services.Abstraction;

public interface IGroupAction : IAsyncDisposable
{
    Task<Either<GroupActionStatus, Group>> UpsertAsync(UpsertGroupViewModel upsert);

    Task<Either<GroupActionStatus, Group>> CreateAsync(UpsertGroupViewModel upsert);

    Task<Either<GroupActionStatus, Group>> UpdateAsync(UpsertGroupViewModel upsert);

    Task<GroupActionStatus> DeleteAsync(Guid id);
}
