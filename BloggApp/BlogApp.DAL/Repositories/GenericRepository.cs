using System.Linq.Expressions;
using BlogApp.Core.Entities.Common;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DAL.Repositories;
public class GenericRepository<T> : IGenericRepository<T>
    where T : BaseEntity, new()
{
    readonly BlogAppDbContext _context;
    protected DbSet<T> Table => _context.Set<T>();
    public GenericRepository(BlogAppDbContext context)
    {
        _context = context; 
    }

    public async Task AddAsync(T entity)
        => await Table.AddAsync(entity);

    public async Task AddRangeAsync(params T[] entities)
        => await Table.AddRangeAsync(entities);

    public IQueryable<T> GetAll(params string[] includes)
    {
        var query = Table.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include); 
        }
        return query; 
    }

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
}

