using System;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        readonly BlogAppDbContext _context;
        readonly IHttpContextAccessor _http; 
        public UserRepository(BlogAppDbContext context, IHttpContextAccessor http) : base(context, http)
        {
            _context = context;
            _http = http; 
        }

        public async Task AddAysnc(User user)
            => await _context.Users.AddAsync(user); 

        public async Task<bool> ExistsByUsername(string username)
            => await _context.Users.AnyAsync(x => x.Username == username);

        public async Task<User?> GetUserByUsernameAsync(string username)
            => await _context.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
    }
}

