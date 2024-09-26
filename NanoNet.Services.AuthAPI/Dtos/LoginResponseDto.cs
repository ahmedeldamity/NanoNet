namespace NanoNet.Services.AuthAPI.Dtos;
public class LoginResponseDto
{
	public UserDto? User { get; set; }
	public string Token { get; set; } = null!;
}