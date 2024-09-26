using System.ComponentModel.DataAnnotations;

namespace NanoNet.Services.AuthAPI.Dtos;
public class RegistrationRequestDto
{
    [EmailAddress] public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    [Phone] public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "Client";
}