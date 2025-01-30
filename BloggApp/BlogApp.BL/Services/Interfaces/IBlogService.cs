 using System;
using BlogApp.BL.DTOs.BlogDtos;
using BlogApp.Core.Entities;

namespace BlogApp.BL.Services.Interfaces;
public interface IBlogService
{
	Task<int> CreateAsync(BlogCreateDto dto);
	Task<IEnumerable<BlogGetDto>> GetAllAsync();
}

