using System;
using BlogApp.BL.DTOs.BlogDtos;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Repositories;
using BlogApp.Core.Entities;
using BlogApp.BL.Exceptions.Common;

namespace BlogApp.BL.Services.Implements;
public class BlogService : IBlogService
{
    readonly IBlogRepository _repo;
    readonly ICategoryRepository _catRepo; 
    public BlogService(IBlogRepository repo, ICategoryRepository catRepo)
    {
        _catRepo = catRepo; 
        _repo = repo; 
    }

    public async Task<int> CreateAsync(BlogCreateDto dto)
    {
        if (!await _catRepo.IsExistAsync(dto.CategoryId))
            throw new NotFoundException<Category>();
        var user = await _repo.GetCurrentUserAsync();
        if (user == null)
            throw new NotFoundException<User>();

        var blog = new Blog
        {
            Title = dto.Title,
            Content = dto.Content,
            CategoryId = dto.CategoryId,
            ViewCount = 0,
            Publisher = user
        }; 
        await _repo.AddAsync(blog);
        await _repo.SaveAsync();
        return blog.Id; 
    }
}

