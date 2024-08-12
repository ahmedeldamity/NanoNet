using System.ComponentModel.DataAnnotations;

namespace NanoNet.Web.ViewModels
{
    public class RegistrationRequestViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string? Role { get; set; }
    }
}
