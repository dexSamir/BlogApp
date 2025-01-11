using System;
using BlogApp.BL.DTOs.UserDtos;

namespace BlogApp.BL.Services.Interfaces;

public interface IAuthService 
{
	Task<string> LoginAsync(LoginDto dto);
	Task RegisterAsync(RegisterDto dto);
	Task<int> SendVereficationEmailAsync(string email);  
    Task<bool> VerifyAccoundAsync(string email, int code); 
}

