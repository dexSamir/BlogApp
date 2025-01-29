using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Context;

namespace BlogApp.DAL.Repositories;
public class BlogRepository : GenericRepository<Blog>, IBlogRepository
{
    public BlogRepository(BlogAppDbContext context) : base(context)
    {
    }
}

