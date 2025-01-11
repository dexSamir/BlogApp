using System;
using BlogApp.BL.DTOs.BlogDtos;

namespace BlogApp.BL.Services.Interfaces;
public interface IBlogService
{
	Task<int> CreateAsync(BlogCreateDto dto); 
}

