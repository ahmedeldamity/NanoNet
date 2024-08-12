using Microsoft.AspNetCore.Mvc;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Controllers
{
    public class AuthController(IAuthService _authService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            LoginRequestViewModel model = new LoginRequestViewModel();


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return View();
        }
    }
}
