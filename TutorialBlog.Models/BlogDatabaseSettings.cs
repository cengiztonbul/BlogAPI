namespace TutorialBlog.Models
{
	public class BlogDatabaseSettings : IDatabaseSettings
	{
		public string ConnectionString { get; set; }

		public string DatabaseName { get; set; }

		public string CollectionName { get; set; }
	}
}
