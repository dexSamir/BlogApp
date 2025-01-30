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

    public async Task<IEnumerable<T>> GetAllAsync(params string[] includes)
        => await _checkIncludes(Table, includes).ToListAsync(); 

    public async Task<T?> GetByIdAsync(int id, params string[] includes)
    {
         if(includes == null)
            return await Table.FindAsync(id);

        return await _checkIncludes(Table, includes).FirstOrDefaultAsync(x=> x.Id == id); 
    }
    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        return await _checkIncludes(Table.Where(expression), includes).ToListAsync();
    }

    public async Task<bool> IsExistAsync(int id)
        => await Table.AnyAsync(x => x.Id == id);

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        => await Table.AnyAsync(expression);

    public async void Delete(T entity, params string[] includes)
    {
        _checkIncludes(Table, includes);
        Table.Remove(entity); 
    }

    public async Task DeleteAndSaveAsync(int id)
    {
        await Table.Where(x=>x.Id == id).ExecuteDeleteAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        Table.Remove(entity!);
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();

    public Task<T> GetFirstAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        throw new NotImplementedException();
    }

    IQueryable<T> _checkIncludes(IQueryable<T> query, params string[] includes)
    {
        if (includes is not null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        return query;
    }
}

