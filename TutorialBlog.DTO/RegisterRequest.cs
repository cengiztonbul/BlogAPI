using System;

namespace TutorialBlog.DTO
{
	public class RegisterRequest
	{
		public DateTime RegistrationDate { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string PasswordRepeat { get; set; }

		public string Email { get; set; }
	}
}
