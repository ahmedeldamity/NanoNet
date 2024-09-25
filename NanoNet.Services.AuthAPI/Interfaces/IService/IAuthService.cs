using NanoNet.Services.AuthAPI.Dtos;
using NanoNet.Services.AuthAPI.ErrorHandling;

namespace NanoNet.Services.AuthAPI.Interfaces.IService;
public interface IAuthService
{
    Task<Result<UserDto>> Register(RegistrationRequestDto requestDto);
    Task<Result<LoginResponseDto>> Login(LoginRequestDto requestDto);
    Task<Result> AssignRole(string email, string roleName);
}