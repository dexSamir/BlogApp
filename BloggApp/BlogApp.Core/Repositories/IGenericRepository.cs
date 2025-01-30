using System;
using System.Linq.Expressions;
using BlogApp.Core.Entities;

namespace BlogApp.Core.Repositories;
public interface IGenericRepository<T> where T : BaseEntity, new()
{
	Task<IEnumerable<T>> GetAllAsync(params string[] includes);
	Task<T?> GetByIdAsync(int id, params string[] includes);
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, params string[] includes);
	Task<T> GetFirstAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task<bool> IsExistAsync(int id);
	Task AddAsync(T entity);
	Task AddRangeAsync(params T[] entities);
	void Delete(T entity, params string[] includes); 
	Task DeleteAsync(int id);
    Task DeleteAndSaveAsync(int id);
	Task<int> SaveAsync();
}

