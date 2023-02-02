namespace KitchenStorage.Services.Abstraction;

public interface IGroupGet : IAsyncDisposable
{
    Task<IEnumerable<Group>> GroupsAsync();

    Task<Group?> FindByIdAsync(Guid id);
}
