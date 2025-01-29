using System;
namespace BlogApp.BL.DTOs.UserDtos;
public class UserGetDto
{
	public string Fullname { get; set; }
	public string Email { get; set; }
	public string Username { get; set; }
	public string? Image { get; set; }
    public int Age { get; set; }
	public int Role { get; set; }
}

