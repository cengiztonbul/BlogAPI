using TutorialBlog.Models;

namespace TutorialBlog.DAL.Abstract
{
	public interface IUserDAL :IEntityRepository<User>
	{
		User Authenticate(User user);
	}
}
