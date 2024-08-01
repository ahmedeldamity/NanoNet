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
		public async Task<IActionResult> Login(LoginRequestDto requestDto)
		{
			var responseDto = new ResponseDto();

			if (requestDto == null)
			{
				responseDto.IsSuccess = false;
				responseDto.Message = "Invalid request";
				return BadRequest(responseDto);
			}

			var loginResponse = await _authService.Login(requestDto);

			if (loginResponse.User is null)
			{
				responseDto.IsSuccess = false;
				responseDto.Message = "Invalid email or password";
				return BadRequest(responseDto);
			}

			responseDto.Result = loginResponse;
			return Ok(responseDto);
		}
	}
}
