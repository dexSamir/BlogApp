using System;
using AutoMapper;
using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Helpers;
using BlogApp.Core.Entities;

namespace BlogApp.BL.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<RegisterDto, User>()
				.ForMember(x=> x.PasswordHash, x=> x.MapFrom(y=>
				  HashHelper.HashPassword(y.Password))); 
		}
	}
}

