using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.Helpers;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.BL.Services.Implements
{
    public class AuthService : IAuthService
    {
        readonly IUserRepository _repo;
        readonly IMapper _mapper;
        public AuthService(IUserRepository repo, IMapper mapper)
        {
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

            List<Claim> claims =
                [
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim (ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim("Fullname", user.Fullname)
                ];
            

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Salam"));

            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: "https://localhost:7067",
                audience: "https://localhost:7067",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(36),
                signingCredentials: cred
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(jwt);
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var user = await _repo.GetAll()
                .Where(x => x.Username == dto.Username || x.Email == dto.Username)
                .FirstOrDefaultAsync();
            if(user != null)
            {
                if (user.Email == dto.Email)
                    throw new ExistException($"{dto.Email} is already exists");
                if (user.Username == dto.Username)
                    throw new ExistException($"{dto.Username} is already exists");
            }
            user = _mapper.Map<User>(dto);
            await _repo.AddAsync(user);
            await _repo.SaveAsync(); 
        }
    }
}

