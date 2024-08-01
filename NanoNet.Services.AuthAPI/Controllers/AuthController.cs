using Microsoft.AspNetCore.Mvc;

namespace NanoNet.Services.AuthAPI.Controllers
{
	public class AuthController : BaseController
	{
		[HttpPost("register")]
		public async Task<IActionResult> Register()
		{
			return Ok();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login()
		{
			return Ok();
		}
	}
}
