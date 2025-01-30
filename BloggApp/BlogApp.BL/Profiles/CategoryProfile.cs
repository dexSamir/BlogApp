using System;
using AutoMapper;
using BlogApp.BL.DTOs.CategoryDtos;
using BlogApp.Core.Entities;

namespace BlogApp.BL.Profiles;
public class CategoryProfile : Profile
{
	public CategoryProfile()
	{
		CreateMap<Category, CategoryNestedGetDto>();
		CreateMap<Category, CategoryListItem>(); 
	}
}

