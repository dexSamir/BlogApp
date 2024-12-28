using System;
using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id); 
            builder.HasIndex(x => x.Username)
                .IsUnique();
            builder.HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}

