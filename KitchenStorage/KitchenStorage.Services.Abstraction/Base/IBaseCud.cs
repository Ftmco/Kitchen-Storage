using System.Linq.Expressions;

namespace KitchenStorage.Services.Abstraction.Base;

public interface IBaseCud<TEntity> : IAsyncDisposable where TEntity : class
{
    Task<bool> InsertAsync(TEntity entity);

    Task<bool> InsertAsync(IEnumerable<TEntity> entities);

    Task<bool> UpdateAsync(TEntity entity);

    Task<bool> UpdateAsync(IEnumerable<TEntity> entities);

    Task<bool> DeleteAsync(object id);

    Task<bool> DeleteAsync(TEntity entity);

    Task<bool> DeleteAsync(IEnumerable<TEntity> entities);

    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where);
    Task<int> CountAsync();

    Task<int> CountAsync(Expression<Func<TEntity, bool>> count);

    Task<bool> SaveAsync();
}
