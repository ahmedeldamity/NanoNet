using System.ComponentModel.DataAnnotations;

namespace NanoNet.Web.ViewModels
{
    public class LoginRequestViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
