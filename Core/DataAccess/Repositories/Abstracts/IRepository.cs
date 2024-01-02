using Entities;
using System.Linq.Expressions;

namespace Core.DataAccess.Repositories.Abstracts;

public interface IRepository<T> where T: class,new()
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,params string[] includes);
    Task<List<T>> GetAllPaginatedAsync(int size, int page, Expression<Func<T, bool>> filter = null, params string[] includes);
    Task<T> GetAsync(Expression<Func<T, bool>> filter, params string[] includes);
    Task<bool> ExistAsync(Expression<Func<T, bool>> filter);
    Task AddAsync(T entity);
    Task<int> SaveAsync();
    void Update(T entity,params string[] notUpdatedProperties);
    void Delete(T entity);
}
