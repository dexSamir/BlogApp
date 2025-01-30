using System;
using AutoMapper;
using BlogApp.BL.DTOs.CategoryDtos;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.BL.Services.Implements;
public class CategoryService : ICategoryService
{
    readonly IMapper _mapper; 
	readonly ICategoryRepository _repo;
	public CategoryService(ICategoryRepository repo, IMapper mapper)
	{
        _mapper = mapper; 
        _repo = repo; 
	}

    public async Task<int> CreateAsync(CategoryCreateDto dto)
    {
        Category cat = dto;
        await _repo.AddAsync(cat);
        await _repo.SaveAsync(); 
        return cat.Id; 
    }

    public async Task<IEnumerable<CategoryListItem>> GetAllAsync()
    {
        var categories = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryListItem>>(categories); 
    }
}

