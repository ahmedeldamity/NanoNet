using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Utility;
using NanoNet.Web.ViewModels;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NanoNet.Web.Controllers;
public class AuthController(IAuthService _authService, ITokenProvider _tokenProvider) : Controller
{

    public IActionResult Register()
    {
        var list = new List<SelectListItem>()
        {
            new SelectListItem { Text = "Admin", Value = SD.RoleAdmin },
            new SelectListItem { Text = "Client", Value = SD.RoleUser }
        };

        ViewBag.RoleList = list;

        return View();
    }

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

                TempData["Success"] = "User created successfully";

                return RedirectToAction(nameof(Login));
            }
            else
            {
                TempData["error"] = result.Message;
            }
        }
        else
        {
            TempData["error"] = "Invalid registration attempt";
        }

        var list = new List<SelectListItem>()
        {
            new SelectListItem { Text = "Admin", Value = SD.RoleAdmin },
            new SelectListItem { Text = "User", Value = SD.RoleUser }
        };

        ViewBag.RoleList = list;

        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(model);

            if (result is not null)
            {
                if (result.IsSuccess)
                {
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponseViewModel>(Convert.ToString(result.Result));

                    await SignInUser(loginResponse);

                    _tokenProvider.SetToken(loginResponse.Token);

                    TempData["Success"] = "Login successful";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }
        }
        TempData["Error"] = "Invalid login attempt";

        return View(model);
    }

    private async Task SignInUser(LoginResponseViewModel model)
    {

        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(model.Token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value));

        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value));

        identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name).Value));

        identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role).Value));

        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        _tokenProvider.ClearToken();
        return RedirectToAction("Index", "Home");
    }
}
