using System;
using BlogApp.BL.DTOs.UserDtos;

namespace BlogApp.BL.Services.Interfaces
{
	public interface IAuthService 
	{
		Task<string> LoginAsync(LoginDto dto);
		Task RegisterAsync(RegisterDto dto); 
	}
}

