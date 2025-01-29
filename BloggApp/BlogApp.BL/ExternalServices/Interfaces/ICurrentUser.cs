using System;
using BlogApp.BL.DTOs.UserDtos;

namespace BlogApp.BL.ExternalServices.Interfaces;
public interface ICurrentUser
{
	int GetId();
	string GetUserName();
	string GetEmail();
	string GetFullname();
	int GetRole();
	Task<UserGetDto> GetUserAsync(); 

}

