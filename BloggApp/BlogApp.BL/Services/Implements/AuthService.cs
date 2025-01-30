using AutoMapper;
using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BlogApp.BL.Services.Implements;
public class AuthService : IAuthService
{
    readonly IMemoryCache _cache; 
    readonly IEmailService _service; 
    readonly IUserRepository _repo;
    readonly IMapper _mapper;
    readonly IJwtTokenHandler _tokenHandler;
    readonly IHttpContextAccessor _httpContextAccessor; 

    public AuthService(
        IUserRepository repo,
        IMapper mapper,
        IJwtTokenHandler tokenHandler,
        IEmailService service,
        IHttpContextAccessor httpContextAccessor,
        IMemoryCache cache)
    {
        _cache = cache; 
        _httpContextAccessor = httpContextAccessor; 
        _service = service;
        _tokenHandler = tokenHandler; 
        _mapper = mapper; 
        _repo = repo; 
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {

        var user = await _repo
            .GetFirstAsync(x => x.Username == dto.UsernameOrEmail || x.Email == dto.UsernameOrEmail);

        if (user == null)
            throw new NotFoundException<User>();

        return _tokenHandler.CreateToken(user, 12);
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var user = await _repo
            .GetFirstAsync(x => x.Username == dto.Username || x.Email == dto.Email); 
           
        if (user != null)
        {
            if (user.Email == dto.Email)
                throw new ExistException($"{dto.Email} already exists.");
            if (user.Username == dto.Username)
                throw new ExistException($"{dto.Username} already exists.");
        }

        var newUser = _mapper.Map<User>(dto);
        string token = Guid.NewGuid().ToString();
        newUser.ConfirmationToken = token;
        newUser.IsConfirmed = false;

        await _repo.AddAsync(newUser);
        await _repo.SaveAsync();

        await _service.SendEmailVereficationAsync(dto.Email);
    }

    public async Task<string> SendVereficationEmailAsync(string email)
    {
        string result = await _service.SendEmailVereficationAsync(email);
        return result;
    }

    public async Task<bool> VerifyAccountAsync(string email, int code)
    {
        await _service.VerifyEmailAsync(email, code); 
        return true; 
    }
}

