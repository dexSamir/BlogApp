using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.BL.Helpers;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.BL.Services.Implements
{
    public class AuthService : IAuthService
    {
        readonly IEmailService _service; 
        readonly IUserRepository _repo;
        readonly IMapper _mapper;
        readonly IJwtTokenHandler _tokenHandler;
        readonly IHttpContextAccessor _httpContextAccessor; 

        public AuthService(IUserRepository repo, IMapper mapper, IJwtTokenHandler tokenHandler, IEmailService service, IHttpContextAccessor httpContextAccessor)
        {
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
        }

    }
}

