using System;
using System.Collections.Generic;
using System.Text;
using TutorialBlog.DTO;

namespace TutorialBlog.Business.Abstract
{
	public interface IUserService
	{
		UserDTO Authenticate(LoginRequest loginData);

		UserDTO Register(RegisterRequest registerRequest);
	}
}
