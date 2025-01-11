using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Exceptions.AuthExceptions;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.BL.Helpers;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Helpers.Enums;
using BlogApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.BL.Services.Implements
{
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

            var user = await _repo.GetAll()
                .Where(x => x.Username == dto.UsernameOrEmail || x.Email == dto.UsernameOrEmail)
                .FirstOrDefaultAsync();
            if (user == null)
                throw new NotFoundException<User>();

           

            return _tokenHandler.CreateToken(user, 12);
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var user = await _repo.GetAll()
                .Where(x => x.Username == dto.Username || x.Email == dto.Email)
                .FirstOrDefaultAsync();

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

            _service.SendEmailConfirmation(_httpContextAccessor.HttpContext.Request, dto.Email, dto.Username, token);
        }

        public async Task ConfirmEmailAsync(string token)
        {
            var user = await _repo.GetAll()
                .Where(x => x.ConfirmationToken == token)
                .FirstOrDefaultAsync();

            if (user == null)
                throw new NotFoundException("Invalid confirmation token.");

            if (user.IsConfirmed)
                throw new InvalidOperationException("Email is already confirmed.");

            user.IsConfirmed = true;
            user.ConfirmationToken = null; 
            await _repo.SaveAsync();
            await SendVereficationEmailAsync(user.Email); 
        }

        public async Task<int> SendVereficationEmailAsync(string email)
        {
            if (_cache.TryGetValue(email, out var _))
                throw new ExistException("Email artig gonderilib!");
            if (!await _repo.IsExistAsync(z => z.Email == email))
                throw new NotFoundException<User>(); 

            Random r = new Random();
            int code = r.Next(100000, 999999);

            _cache.Set(email, code, TimeSpan.FromMinutes(10));
            return code; 
        }

        public async Task<bool> VerifyAccoundAsync (string email, int code)
        {
            if (!_cache.TryGetValue<int>(email, out int result))
                throw new NotFoundException("Kod gondermemisik");
            if(result != code)
                throw new CodeIsInvalidException();

            var user = await _repo.GetWhere(x => x.Email == email).FirstOrDefaultAsync();
            user!.IsVerified = true;
            user.Role = user.Role | (int)Roles.Publisher;
            await _repo.SaveAsync();
            return true; 
        }
    }
}

