using System;
using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.BL.Services.Implements
{
    public class AuthService : IAuthService
    {
        readonly IUserRepository _repo;
        public AuthService(IUserRepository repo)
        {
            _repo = repo; 
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _repo.GetAll()
                .Where(x => x.Username == dto.UsernameOrEmail || x.Email == dto.UsernameOrEmail)
                .FirstOrDefaultAsync();
            throw new NotImplementedException();

        }

        public Task RegisterAsync(RegisterDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

