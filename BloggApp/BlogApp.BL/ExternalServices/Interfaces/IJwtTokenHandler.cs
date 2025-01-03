using System;
using BlogApp.Core.Entities;

namespace BlogApp.BL.ExternalServices.Interfaces;

public interface IJwtTokenHandler
{
	string CreateToken(User user, int hours); 
}

