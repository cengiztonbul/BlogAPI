namespace TutorialBlog.Models
{
	public class UserDatabaseSettings : IDatabaseSettings
	{
		public string ConnectionString { get; set; }

		public string DatabaseName { get; set; }

		public string CollectionName { get; set; }
	}
}
