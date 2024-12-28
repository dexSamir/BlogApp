﻿using System;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        readonly BlogAppDbContext _context; 
        public UserRepository(BlogAppDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
        }
    }
}
