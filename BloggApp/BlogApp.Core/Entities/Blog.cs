namespace BlogApp.Core.Entities;
public class Blog : BaseEntity
{
	public string Title { get; set; }
	public string Content { get; set; }
	public int ViewCount  { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
	public int CategoryId { get; set; }
	public Category Category { get; set; }
	public string CoverImage { get; set; }
}

