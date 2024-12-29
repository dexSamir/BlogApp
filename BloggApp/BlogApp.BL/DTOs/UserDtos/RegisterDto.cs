using System;
namespace BlogApp.BL.DTOs.UserDtos;

public class RegisterDto
{
    public string Username { get; set; }
    public string Fullname { get; set; }
    public int Age { get; set; }
    public bool IsFemale { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }
}

