using System.ComponentModel.DataAnnotations;

namespace NanoNet.Web.ViewModels;
public class RegistrationRequestViewModel
{
    [EmailAddress] public string Email { get; set; } = null!;

    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
    public string Name { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^(\d{11})$", ErrorMessage = "Phone number must be 11 digits.")]
    public string PhoneNumber { get; set; } = null!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
    public string ConfirmPassword { get; set; } = null!;

    public string? Role { get; set; }
}