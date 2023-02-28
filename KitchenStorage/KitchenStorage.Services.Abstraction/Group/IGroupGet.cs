namespace KitchenStorage.Services.Abstraction;

public interface IGroupGet : IAsyncDisposable
{
    Task<PaginationResult<IEnumerable<Group>>> GroupsAsync(int page, int count, string? q);
}
