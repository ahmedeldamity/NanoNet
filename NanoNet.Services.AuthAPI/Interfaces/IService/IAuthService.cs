using NanoNet.Services.AuthAPI.Dtos;

namespace NanoNet.Services.AuthAPI.Interfaces.IService
{
	public interface IAuthService
	{
		Task<ResponseDto> Register(RegisterationRequestDto requestDto);
		Task<LoginResponseDto> Login(LoginRequestDto requestDto);
	}
}
