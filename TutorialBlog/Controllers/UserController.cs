using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutorialBlog.Business.Abstract;
using TutorialBlog.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace TutorialBlog.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			this._userService = userService;
		}

		[AllowAnonymous]
		[Route("login")]
		[HttpPost]
		public UserDTO LoginPost([FromBody] LoginRequest loginData)
		{
			return _userService.Authenticate(loginData);
		}

		[AllowAnonymous]
		[Route("login")]
		[HttpGet]
		public int LoginGet()
		{
			return 0;
		}

		[AllowAnonymous]
		[Route("register")]
		[HttpPost]
		public UserDTO RegisterPost([FromBody] RegisterRequest registerData)
		{
			return _userService.Register(registerData);
		}

		[AllowAnonymous]
		[Route("register")]
		[HttpGet]
		public int RegisterGet()
		{
			return 0;
		}

		[HttpGet]
		public string Get()
		{
			return User.Identity.Name;
		}
    }
}