﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;
using Newtonsoft.Json;
using System.Security.Claims;

namespace NanoNet.Web.Controllers;
public class CartController(ICartService _cartService) : Controller
{
    [Authorize]
    public async Task<IActionResult> CartIndex()
    {
        return View(await LoadCartBasedOnLoggedInUser());
    }

    private async Task<CartViewModel> LoadCartBasedOnLoggedInUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await _cartService.GetCartByUserIdAsync(userId);
        if (response is not null && response.IsSuccess)
        {
            CartViewModel cartViewModel = JsonConvert.DeserializeObject<CartViewModel>(response.Result.ToString());
            return cartViewModel;
        }
        return new CartViewModel();
    }
}
