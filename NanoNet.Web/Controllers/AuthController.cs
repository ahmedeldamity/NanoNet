using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Utility;
using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Controllers
{
    public class AuthController : Controller
    {

        public async Task<IActionResult> Register()
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Admin", Value = SD.RoleAdmin },
                new SelectListItem { Text = "User", Value = SD.RoleUser }
            };

            ViewBag.RoleList = list;

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
