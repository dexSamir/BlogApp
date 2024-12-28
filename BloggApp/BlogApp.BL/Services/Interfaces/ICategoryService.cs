using System;
using BlogApp.BL.DTOs.CategoryDtos;

namespace BlogApp.BL.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryListItem>> GetAllAsync();
    Task<int> CreateAsync(CategoryCreateDto dto); 
}

