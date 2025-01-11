using System;
using System.Linq.Expressions;
using System.Security.Claims;
using BlogApp.Core.Entities;
using BlogApp.Core.Entities.Common;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity, new()
    {
        readonly IHttpContextAccessor _http; 
        readonly BlogAppDbContext _context;
        protected DbSet<T> Table => _context.Set<T>();
        public GenericRepository(BlogAppDbContext context, IHttpContextAccessor http)
        {
            _http = http; 
            _context = context; 
        }

        public async Task AddAsync(T entity)
            => await Table.AddAsync(entity);

        public async Task AddRangeAsync(params T[] entities)
            => await Table.AddRangeAsync(entities);

        public IQueryable<T> GetAll()
            => Table.AsQueryable();

        public async Task<T?> GetByIdAsync(int id)
            => await Table.FindAsync(id);

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
            => Table.Where(expression).AsQueryable();

        public async Task<bool> IsExistAsync(int id)
            => await Table.AnyAsync(x => x.Id == id); 

        public void Remove(T entity)
            => Table.Remove(entity);

        public async Task<bool> RemoveAsync(int id)
        {
            int result = await Table.Where(x=>x.Id == id).ExecuteDeleteAsync();
            return result > 0; 
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
            => await Table.AnyAsync(expression);

        public string GetCurrentUserName()
        {
            return _http.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value; 
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            string userName = GetCurrentUserName();
            if (string.IsNullOrWhiteSpace(userName))
                return null;
            return await _context.Users.Where(x => x.Username == userName).FirstOrDefaultAsync(); 
        }
    }
}

