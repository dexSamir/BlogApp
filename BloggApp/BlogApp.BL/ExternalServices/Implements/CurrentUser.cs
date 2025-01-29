using System;
using System.Security.Claims;
using AutoMapper;
using BlogApp.BL.Constant;
using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.ExternalServices.Implements;
public class CurrentUser(IHttpContextAccessor _http, IUserRepository _repo, IMapper _mapper)  : ICurrentUser
{
    ClaimsPrincipal? User = _http.HttpContext?.User; 

    public string GetEmail()
    {
        var value = User.FindFirst(x => x.Type == ClaimType.Email)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return value;
    }

    public string GetFullname()
    {
        var value = User.FindFirst(x => x.Type == ClaimType.Fullname)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return value;
    }

    public int GetId()
    {
        var value = User.FindFirst(x => x.Type == ClaimType.Id)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return Convert.ToInt32(value);
    }

    public int GetRole()
    {
        var value = User.FindFirst(x => x.Type == ClaimType.Role )?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return Convert.ToInt32(value);
    }

    public async Task<UserGetDto> GetUserAsync()
    {
        int userId = GetId();
        var user = await _repo.GetByIdAsync(userId);
        return _mapper.Map<UserGetDto>(user);
    }

    public string GetUserName()
    {
        var value = User.FindFirst(x => x.Type == ClaimType.Username)?.Value;
        if (value is null)
            throw new NotFoundException<User>();
        return value;
    }
}

