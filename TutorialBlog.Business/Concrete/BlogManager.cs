using TutorialBlog.Business.Abstract;
using TutorialBlog.DTO;
using TutorialBlog.Models;
using TutorialBlog.DAL.Abstract;
using System.Collections.Generic;

namespace TutorialBlog.Business.Concrete
{
	public class BlogManager : IBlogService
	{
		private readonly IBlogDAL _blogDAL;

		private Blog BlogMapper(BlogDTO blog)
		{
			return new Blog()
			{
				Author = blog.Author,
				BlogTitle = blog.BlogTitle,
				BlogText = blog.BlogText,
				BlogDate = blog.BlogDate,
				Id = blog.Id
			};
		}

		private BlogDTO BlogMapper(Blog blog)
		{
			return new BlogDTO()
			{
				Author = blog.Author,
				BlogTitle = blog.BlogTitle,
				BlogText = blog.BlogText,
				BlogDate = blog.BlogDate,
				Id = blog.Id
			};
		}

		public BlogManager(IBlogDAL blogDAL)
		{
			_blogDAL = blogDAL;
		}

		public BlogDTO Create(BlogDTO blog)
		{
			if (string.IsNullOrEmpty(blog.BlogTitle) || string.IsNullOrEmpty(blog.BlogText))
			{
				return null;
			}

			Blog dboBlog = BlogMapper(blog);

			dboBlog = _blogDAL.Add(dboBlog);

			return BlogMapper(dboBlog);
		}

		public void Delete(string id)
		{
			_blogDAL.Delete(id);
		}

		public BlogDTO Get(string id)
		{
			Blog result = _blogDAL.Get(id);

			return BlogMapper(result);
		}

		public List<BlogDTO> GetAll()
		{
			List<Blog> blogs = _blogDAL.GetAll();

			List<BlogDTO> blogsDTO = new List<BlogDTO>();

			foreach(var blog in blogs)
			{
				blogsDTO.Add(BlogMapper(blog));
			}

			return blogsDTO;
		}

		public void Update(string id, BlogDTO blogIn)
		{
			_blogDAL.Update(id, BlogMapper(blogIn));
		}
	}
}
