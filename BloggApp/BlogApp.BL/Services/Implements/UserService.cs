using System;
using System.Security.Cryptography;
using System.Xml;
using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Helpers;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;

namespace BlogApp.BL.Services.Implements;

public class UserService : IUserService
{
    public IUserRepository _repo;
    public UserService(IUserRepository repo)
    {
        _repo = repo; 
    }

    public async Task Register(RegisterDto dto)
    {
        if(await _repo.ExistsByUsername(dto.Username))
            throw new Exception("Email already exists.");
        User user = new User
        {
            Username = dto.Username,
            Fullname = dto.Fullname,
            Email = dto.Email,
            Age = dto.Age,
            IsFemale = dto.IsFemale,
            IsBanned = false,
            PasswordHash = HashHelper.HashPassword(dto.Password), 
            UnlockTime = DateTime.Now
        };
        await _repo.AddAsync(user);
        await _repo.SaveAsync();
    }
    
    public async Task Login(LoginDto dto)
    {
        var user = await _repo.GetUserByUsernameAsync(dto.UsernameOrEmail);
        if (user == null)
            throw new Exception("Username not found!");
        if (!HashHelper.VerifyHashedPassword(user.PasswordHash,dto.Password))
            throw new Exception("Invalid Password");
    }

}

