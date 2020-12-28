namespace TutorialBlog.Models
{
	public interface IDatabaseSettings
	{
		string DatabaseName { get; set; }

		string ConnectionString { get; set; }

		string CollectionName { get; set; }
	}
}
