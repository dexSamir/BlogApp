using System;
using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DAL.Context
{
	public class BlogAppDbContext : DbContext
	{
        public DbSet<Category> Categories { get; set; }

        public BlogAppDbContext( DbContextOptions<BlogAppDbContext> options) : base(options) { }
    }
}

