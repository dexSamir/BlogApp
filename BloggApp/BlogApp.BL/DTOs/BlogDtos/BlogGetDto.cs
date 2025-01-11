using System;
using BlogApp.BL.DTOs.CategoryDtos;
using BlogApp.BL.DTOs.UserDtos;
using BlogApp.Core.Entities;

namespace BlogApp.BL.DTOs.BlogDtos;
public class BlogGetDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public string ViewCount { get; set; }
	public DateTime CreatedTime { get; set; }
	public UserNestedGetDto Publisher { get; set; }
	public CategoryNestedGetDto Category { get; set; }

}

