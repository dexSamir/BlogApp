using System;
using AutoMapper;
using BlogApp.BL.DTOs.BlogDtos;
using BlogApp.Core.Entities;

namespace BlogApp.BL.Profiles;
public class BlogProfile : Profile
{
	public BlogProfile()
	{
		CreateMap<BlogCreateDto, Blog>()
			.ForMember(x=> x.CoverImage, z=> z.Ignore());
		
		CreateMap<Blog, BlogGetDto>();
	}
}

