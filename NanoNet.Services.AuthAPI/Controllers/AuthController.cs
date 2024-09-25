using Microsoft.AspNetCore.Mvc;
using NanoNet.Services.AuthAPI.Dtos;
using NanoNet.Services.AuthAPI.ErrorHandling;
using NanoNet.Services.AuthAPI.Interfaces.IService;

namespace NanoNet.Services.AuthAPI.Controllers;
public class AuthController(IAuthService authService) : BaseController
{
    [HttpPost("register")]
	public async Task<ActionResult<Result>> Register(RegistrationRequestDto requestDto)
    {
        var result = await authService.Register(requestDto);

        return result;
    }

	[HttpPost("login")]
	public async Task<ActionResult<Result>> Login(LoginRequestDto requestDto)
	{
		var result = await authService.Login(requestDto);
        
        return result;
    }

	[HttpPost("AssignRole")]
	public async Task<ActionResult<Result>> AssignRole(RegistrationRequestDto requestDto)
	{
		var result= await authService.AssignRole(requestDto.Email, requestDto.Role.ToUpper());

		return result;
	}
}