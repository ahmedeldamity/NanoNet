using Microsoft.AspNetCore.Mvc;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Controllers
{
    public class AuthController : Controller
    {

        public async Task<IActionResult> Register()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            return View();
        }
    }
}
