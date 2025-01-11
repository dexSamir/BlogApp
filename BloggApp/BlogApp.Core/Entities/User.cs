using System;
using BlogApp.Core.Helpers.Enums;

namespace BlogApp.Core.Entities
{
	public class User : BaseEntity
	{
		public string Username { get; set; }
		public string Fullname { get; set; }
		public int Age { get; set; }
		public DateOnly BirthDate { get; set; }
		public bool IsFemale{ get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public int Role { get; set; } = (int)Roles.Viewer; 
		public bool IsBanned { get; set; }
		public DateTime UnlockTime { get; set; }
        public bool IsConfirmed { get; set; } 
        public string? ConfirmationToken { get; set; }
		public bool IsVerified { get; set; }
		public IEnumerable<Blog>? Blogs { get; set; } 
    }
}

