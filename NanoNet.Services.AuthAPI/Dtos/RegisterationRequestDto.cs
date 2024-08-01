using System.ComponentModel.DataAnnotations;

namespace NanoNet.Services.AuthAPI.Dtos
{
	public class RegisterationRequestDto
	{
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string RoleName { get; set; } = "Client";
    }
}
