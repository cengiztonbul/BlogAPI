using System.Collections.Generic;
using System.Linq;
using TutorialBlog.Models;

namespace TutorialBlog.TestDatabase
{
	public static class TestData
	{
		private static List<User> _users = new List<User>();
		private static int _id = 1;

		static TestData()
		{
			Init();
		}

		static string GetID()
		{
			return (++_id).ToString();
		}

		public static void AddUser(User user)
		{
			user.Id = GetID();
			_users.Add(user);
		}

		public static void Init()
		{
			AddUser(new User { Username = "cen", FirstName = "Cengiz", LastName = "TONBUL", Password = "123456" });
			AddUser(new User { Username = "bir", FirstName = "Birol", LastName = "GUNES", Password = "123456" });
			AddUser(new User { Username = "xyz", FirstName = "xyz", LastName = "abc", Password = "123456" });
		}

		public static User GetUser(string username, string password)
		{
			User result = _users.SingleOrDefault(user => (user.Username == username) && (user.Password == password));

			return result;
		}
	}
}
