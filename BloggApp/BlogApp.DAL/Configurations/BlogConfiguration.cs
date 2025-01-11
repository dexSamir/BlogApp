using System;
using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DAL.Configurations;
public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Blogs)
            .HasForeignKey(x => x.CategoryId);
        builder.HasOne(x => x.Publisher)
            .WithMany(x => x.Blogs)
            .HasForeignKey(x => x.PublisherId); 
    }
}

