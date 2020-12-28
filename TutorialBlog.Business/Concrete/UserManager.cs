using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TutorialBlog.Business.Abstract;
using TutorialBlog.DTO;
using TutorialBlog.DAL.Abstract;
using TutorialBlog.Models;
using System.Security.Claims;

namespace TutorialBlog.Business.Concrete
{
	public class UserManager : IUserService
	{
		private readonly IUserDAL _userDAL;

		private readonly string secretKey = "classifiedclassifiedclassifiedclassified";

		public UserManager(IUserDAL userDAL)
		{
			this._userDAL = userDAL;
		}

		public UserDTO Authenticate(LoginRequest loginData)
		{
			User user = _userDAL.Authenticate(new User() { Username = loginData.Username, Password = loginData.Password });

			if (user == null)
			{
				return null;
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secretKey);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Id.ToString())
				}),

				Expires = DateTime.UtcNow.AddHours(1),

				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			user.Token = tokenHandler.WriteToken(token);

			_userDAL.Update(user.Id, user);

			return new UserDTO { Id = user.Id, Username = user.Username, FirstName = user.FirstName, LastName = user.LastName, Token = user.Token };
		}

		public UserDTO Register(RegisterRequest registerRequest)
		{
			User newUser = new User { FirstName = registerRequest.FirstName, LastName = registerRequest.LastName, Password = registerRequest.Password, Username = registerRequest.Username };

			_userDAL.Add(newUser);
			return Authenticate(new LoginRequest { Username = newUser.Username, Password = newUser.Password });
		}
	}
}
