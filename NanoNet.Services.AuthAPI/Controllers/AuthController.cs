using Microsoft.AspNetCore.Mvc;
using NanoNet.Services.AuthAPI.Dtos;
using NanoNet.Services.AuthAPI.Interfaces.IService;

namespace NanoNet.Services.AuthAPI.Controllers
{
	public class AuthController : BaseController
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
        {
			_authService = authService;
		}

        [HttpPost("register")]
		public async Task<IActionResult> Register(RegisterationRequestDto requestDto)
		{
			var response = await _authService.Register(requestDto);

			if (response.IsSuccess)
				return Ok(response);

			return BadRequest(response);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login()
		{
			return Ok();
		}
	}
}
