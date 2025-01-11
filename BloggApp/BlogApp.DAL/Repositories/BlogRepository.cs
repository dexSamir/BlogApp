using System;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Context;
using Microsoft.AspNetCore.Http;

namespace BlogApp.DAL.Repositories;
public class BlogRepository : GenericRepository<Blog>, IBlogRepository
{
    public BlogRepository(BlogAppDbContext context,  IHttpContextAccessor _http) : base(context, _http)
    {
    }
}

