using Microsoft.AspNetCore.Mvc;
using StokKontrolProje.Entities.Entities;
using StokKontrolProje.Services.Abstract;
using StokKontrolProje.WebAPI.Security;

namespace StokKontrolProje.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IGenericService<User> service;
		private readonly IConfiguration _config;

		public UsersController(IGenericService<User> service, IConfiguration config)
		{
			this.service = service;
			_config = config;
		}


		[HttpGet]
		public IActionResult Login(string email, string password)
		{
			if (service.Any(x => x.Email.Equals(email) && x.Password.Equals(password)))
			{
				User user = service.GetByDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
				Token token = TokenHandler.CreateToken(user, _config);
				return Ok(token);
			}
			return Unauthorized();
		}
	}
}
