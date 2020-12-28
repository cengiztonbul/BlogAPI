using System.Collections.Generic;
using TutorialBlog.DTO;

namespace TutorialBlog.Business.Abstract
{
	public interface IBlogService
	{
		BlogDTO Get(string id);

		List<BlogDTO> GetAll();

		BlogDTO Create(BlogDTO blog);

		void Update(string id, BlogDTO blogIn);

		void Delete(string id);
	}
}
