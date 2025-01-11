﻿using System;
using System.Linq.Expressions;

namespace BlogApp.Core.Repositories
{
	public interface IGenericRepository<T> where T : BaseEntity, new()
	{
		IQueryable<T> GetAll();
		Task<T?> GetByIdAsync(int id);
		IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);
		Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
        Task<bool> IsExistAsync(int id);
		Task AddAsync(T entity);
		Task AddRangeAsync(params T[] entities);
		void Remove(T entity); 
		Task<bool> RemoveAsync(int id);
		Task<int> SaveAsync(); 
	}
}

