namespace NanoNet.Services.AuthAPI.Dtos
{
	public class RegisterationRequestDto
	{
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; } 
    }
}
