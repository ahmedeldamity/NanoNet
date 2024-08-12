using System.ComponentModel.DataAnnotations;

namespace NanoNet.Web.ViewModels
{
    public class LoginRequestViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
