using Core.DataAccess.Repositories.Abstracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.Repositories.Concretes.EfCore;

public class EfBaseRepository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class, new()
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public EfBaseRepository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbSet.AnyAsync(filter);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params string[] includes)
    {
        IQueryable<TEntity> query = GetQuery(includes);
        return await query.Where(filter).FirstOrDefaultAsync();
    }
    public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includes)
    {
        IQueryable<TEntity> query = GetQuery(includes);
        return filter == null
                        ? query.ToListAsync()
                        : query.Where(filter).ToListAsync();
    }

    public Task<List<TEntity>> GetAllPaginatedAsync(int size, int page, Expression<Func<TEntity, bool>> filter = null, params string[] includes)
    {
        IQueryable<TEntity> query = GetQuery(includes);
        return filter == null
                        ? query.Skip((page - 1) * size).Take(size).ToListAsync()
                        : query.Where(filter).Skip((page - 1) * size).Take(size).ToListAsync();
    }
    private IQueryable<TEntity> GetQuery(string[] includes)
    {
        IQueryable<TEntity> query = _dbSet;
        foreach (string item in includes)
        {
            query = query.Include(item);
        }
        return query;
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(TEntity entity, params string[] notUpdatedProperties)
    {
        _context.Entry(entity).State = EntityState.Modified;
        foreach (var item in notUpdatedProperties)
        {
            _context.Entry(entity).Property(item).IsModified = false;
        } 
        _dbSet.Update(entity);
    }
}
