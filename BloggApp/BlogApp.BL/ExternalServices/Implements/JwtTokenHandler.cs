using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogApp.BL.DTOs.Options;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.BL.ExternalServices.Implements;

public class JwtTokenHandler : IJwtTokenHandler
{
    readonly JwtOptions _opt;
    public JwtTokenHandler(IOptions<JwtOptions> opt)
    {
        _opt = opt.Value; 
    }

    public string CreateToken(User user, int hours)
    {
        List<Claim> claims = [
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim("Fullname", user.Fullname)
                ];

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.SecretKey));

        SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: "https://localhost:7067",
                audience: "https://localhost:7067",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(36),
                signingCredentials: cred);

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(jwt); 
    }
}

