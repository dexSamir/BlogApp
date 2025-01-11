using System;
using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DAL.Context
{
	public class BlogAppDbContext : DbContext
	{
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        public BlogAppDbContext( DbContextOptions<BlogAppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogAppDbContext).Assembly); 
            base.OnModelCreating(modelBuilder);
        }
    }
}

