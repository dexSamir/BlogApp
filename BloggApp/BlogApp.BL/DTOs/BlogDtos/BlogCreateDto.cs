using System;
namespace BlogApp.BL.DTOs.BlogDtos;
public class BlogCreateDto
{
	public string Title { get; set; }
	public string Content { get; set; }
	public int CategoryId { get; set; }
}

