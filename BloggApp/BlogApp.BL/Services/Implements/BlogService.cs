using System;
using BlogApp.BL.DTOs.BlogDtos;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Repositories;
using BlogApp.Core.Entities;
using BlogApp.BL.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BlogApp.BL.Services.Implements;
public class BlogService : IBlogService
{
    readonly IBlogRepository _repo;
    readonly IMapper _mapper; 
    readonly ICategoryRepository _catRepo; 
    public BlogService(IBlogRepository repo, ICategoryRepository catRepo, IMapper mapper)
    {
        _catRepo = catRepo;
        _mapper = mapper; 
        _repo = repo; 
    }

    public async Task<int> CreateAsync(BlogCreateDto dto)
    {
        if (!await _catRepo.IsExistAsync(dto.CategoryId))
            throw new NotFoundException<Category>();
        var user = await _repo.GetCurrentUserAsync();
        if (user == null)
            throw new NotFoundException<User>();

        var blog = _mapper.Map<Blog>(dto);
        blog.Publisher = user; 

        await _repo.AddAsync(blog);
        await _repo.SaveAsync();
        return blog.Id; 
    }

    public async Task<List<BlogGetDto>> GetAllAsync()
    {
        var blogs = await _repo.GetAll("Category", "Publisher").ToListAsync();
        return _mapper.Map<List<BlogGetDto>>(blogs);
    }
}

