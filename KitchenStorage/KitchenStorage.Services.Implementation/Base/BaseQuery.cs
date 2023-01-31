using KitchenStorage.DataBase.Context;
using KitchenStorage.Services.Abstraction.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KitchenStorage.Services.Implementation.Base;

internal class BaseQuery<TEntity> : IBaseQuery<TEntity> where TEntity : class
{
    readonly KitchenContext _context;

    readonly DbSet<TEntity> _dbSet;

    public BaseQuery(KitchenContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> any)
        => await _dbSet.AnyAsync(any);

    public async Task<int> CountAsync()
        => await _dbSet.CountAsync();

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> count)
        => await _dbSet.CountAsync(count);

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _context.DisposeAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where)
        => await _dbSet.Where(where).ToListAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsync(int page, int count)
        => count != 0 ? await _dbSet.Skip(page * count).Take(count).ToListAsync() : await _dbSet.ToListAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where, int page, int count)
            => count != 0 ? await _dbSet.Where(where).Skip(page * count).Take(count).ToListAsync() :
                         await _dbSet.Where(where).ToListAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(int page, int count, Expression<Func<TEntity, TKey>> sort, SortType sortType = SortType.Descending)
         => count != 0 ?
            sortType switch
            {
                SortType.Descending => await _dbSet.OrderByDescending(sort).Skip(page * count).Take(count).ToListAsync(),
                SortType.Ascending => await _dbSet.OrderBy(sort).Skip(page * count).Take(count).ToListAsync(),
                _ => await _dbSet.OrderByDescending(sort).Skip(page * count).Take(count).ToListAsync(),
            }
            :
            sortType switch
            {
                SortType.Descending => await _dbSet.OrderByDescending(sort).ToListAsync(),
                SortType.Ascending => await _dbSet.OrderBy(sort).ToListAsync(),
                _ => await _dbSet.OrderByDescending(sort).ToListAsync(),
            };

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> sort, SortType sortType = SortType.Descending)
        => sortType switch
        {
            SortType.Descending => await _dbSet.OrderByDescending(sort).Where(where).ToListAsync(),
            SortType.Ascending => await _dbSet.OrderBy(sort).Where(where).ToListAsync(),
            _ => await _dbSet.OrderByDescending(sort).Where(where).ToListAsync(),
        };

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> where, int page, int count, Expression<Func<TEntity, TKey>> sort, SortType sortType = SortType.Descending)
        => count != 0 ?
                sortType switch
                {
                    SortType.Descending => await _dbSet.OrderByDescending(sort).Where(where).Skip(page * count).Take(count).ToListAsync(),
                    SortType.Ascending => await _dbSet.OrderBy(sort).Where(where).Skip(page * count).Take(count).ToListAsync(),
                    _ => await _dbSet.OrderByDescending(sort).Where(where).Skip(page * count).Take(count).ToListAsync(),
                }
                :
                sortType switch
                {
                    SortType.Descending => await _dbSet.OrderByDescending(sort).Where(where).ToListAsync(),
                    SortType.Ascending => await _dbSet.OrderBy(sort).Where(where).ToListAsync(),
                    _ => await _dbSet.OrderByDescending(sort).Where(where).ToListAsync(),
                };

    public async Task<TEntity?> GetAsync(object? id)
        => await _dbSet.FindAsync(id);

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> firstOrDefault)
        => await _dbSet.FirstOrDefaultAsync(firstOrDefault);
}
