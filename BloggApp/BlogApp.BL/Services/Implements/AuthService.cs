using System;
using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.Helpers;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
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
            if(user == null )
                throw new NotFoundException<User>();

            return HashHelper.VerifyHashedPassword(user.PasswordHash, dto.Password).ToString(); 
        }

        public Task RegisterAsync(RegisterDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

