using System;
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
		public int Role { get; set; } 
		public bool IsBanned { get; set; }
		public DateTime UnlockTime { get; set; }
        public bool IsConfirmed { get; set; }
        public string? ConfirmationToken { get; set; }
    }
}

