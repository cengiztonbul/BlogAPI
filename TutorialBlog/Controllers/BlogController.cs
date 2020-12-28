using TutorialBlog.DTO;
using TutorialBlog.Models;
using TutorialBlog.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace BooksApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		private readonly IBlogService _blogService;

		public BlogController(IBlogService blogService)
		{
			_blogService = blogService;
		}

		[AllowAnonymous]
		[HttpGet]
		public ActionResult<List<BlogDTO>> Get() =>
			_blogService.GetAll();

		[AllowAnonymous]
		[HttpGet("{id:length(24)}", Name = "GetBook")]
		public ActionResult<BlogDTO> Get(string id)
		{
			var blog = _blogService.Get(id);

			if (blog == null)
			{
				return NotFound();
			}

			return blog;
		}

		[HttpPost]
		public ActionResult<BlogDTO> Create([FromBody] BlogDTO blog)
		{
			return _blogService.Create(blog);
		}

		[HttpPut("{id:length(24)}")]
		public IActionResult Update(string id, BlogDTO blogIn)
		{
			var blog = _blogService.Get(id);

			if (blog == null)
			{
				return NotFound();
			}

			_blogService.Update(id, blogIn);

			return NoContent();
		}

		[HttpDelete("{id:length(24)}")]
		public IActionResult Delete(string id)
		{
			var book = _blogService.Get(id);

			if (book == null)
			{
				return NotFound();
			}

			_blogService.Delete(book.Id);

			return NoContent();
		}
	}
}