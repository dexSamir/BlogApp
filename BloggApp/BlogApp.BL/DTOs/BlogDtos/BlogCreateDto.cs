using System;
using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.DTOs.BlogDtos;
public class BlogCreateDto
{
	public string Title { get; set; }
	public string Content { get; set; }
	public int CategoryId { get; set; }
	public IFormFile? CoverImage { get; set; }
}

