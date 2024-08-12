using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Utility;
using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Controllers
{
    public class AuthController(IAuthService _authService) : Controller
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

        // create a new user
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.RegisterAsync(model);

                if (result is not null && result.IsSuccess)
                {
                    if(string.IsNullOrEmpty(model.Role))
                    {
                        model.Role = SD.RoleUser;
                    }
                    var assignRole = await _authService.AssignRoleAsync(model);

                    if (assignRole is not null && assignRole.IsSuccess)
                    {
                        TempData["Success"] = "User created successfully";
                        return RedirectToAction(nameof(Login));
                    }

                    return RedirectToAction("Login", "Auth");
                }
            }

            var list = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Admin", Value = SD.RoleAdmin },
                new SelectListItem { Text = "User", Value = SD.RoleUser }
            };

            ViewBag.RoleList = list;

            return View(model);
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
