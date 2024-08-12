using System.ComponentModel.DataAnnotations;

namespace NanoNet.Services.AuthAPI.Dtos
{
	public class LoginRequestDto
	{
		[EmailAddress]
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
