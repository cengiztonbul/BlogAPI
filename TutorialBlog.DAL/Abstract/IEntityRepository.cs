using System.Collections.Generic;

namespace TutorialBlog.DAL
{
	public interface IEntityRepository<T>
	{
		List<T> GetAll();

		T Get(string id);

		T Add(T entity);

		T Update(string id, T entity);

		void Delete(string id);
	}
}
