using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BlogApp.BL.DTOs.UserDtos;

namespace BlogApp.BL.Services.Interfaces;
public interface IUserService
{
    Task Register(RegisterDto dto);
    Task Login(LoginDto dto); 
}



