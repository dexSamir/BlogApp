using System;
using BlogApp.Core.Entities;
using BlogApp.Core.Entities.Common;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Context;

namespace BlogApp.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BlogAppDbContext _context) : base(_context)
        {
        }
    }
}

