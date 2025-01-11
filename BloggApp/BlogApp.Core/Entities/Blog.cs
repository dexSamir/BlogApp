using System;
namespace BlogApp.Core.Entities;
public class Blog : BaseEntity
{
	public string Title { get; set; }
	public string Content { get; set; }
	public int ViewCount  { get; set; }
	public int PublisherId { get; set; }
	public int CategoryId { get; set; }
	public User Publisher { get; set; }
	public Category Category { get; set; }

}

