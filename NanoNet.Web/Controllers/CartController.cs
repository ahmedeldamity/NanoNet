using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;
using Newtonsoft.Json;
using System.Security.Claims;

namespace NanoNet.Web.Controllers;
public class CartController(ICartService _cartService, IOrderService _orderService) : Controller
{
    [Authorize]
    public async Task<IActionResult> CartIndex()
    {
        return View(await LoadCartBasedOnLoggedInUser());
    }

    [Authorize]
    public async Task<IActionResult> Checkout()
    {
        return View(await LoadCartBasedOnLoggedInUser());
    }

    [HttpPost]
    [ActionName("Checkout")]
    public async Task<IActionResult> Checkout(CartViewModel cartViewModel)
    {
        var cart = await LoadCartBasedOnLoggedInUser();
        cart.CartHeader.Phone = cartViewModel.CartHeader.Phone;
        cart.CartHeader.Email = cartViewModel.CartHeader.Email;
        cart.CartHeader.Name = cartViewModel.CartHeader.Name;

        var response = await _orderService.CreateOrderAsync(cart);
        if (response is not null && response.IsSuccess)
        {
            TempData["Success"] = "Order created successfully";
            return RedirectToAction(nameof(CartIndex));
        }
        return View();
    }

    [Authorize]
    public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
    {
        var response = await _cartService.RemoveCartItemAsync(cartItemId);
        if (response is not null && response.IsSuccess)
        {
            TempData["Success"] = "Item removed from cart successfully";
            return RedirectToAction(nameof(CartIndex));
        }
        return RedirectToAction("CartIndex");
    }

    [HttpPost]
    public async Task<IActionResult> ApplyCoupon(CartViewModel cartViewModel)
    {
        var response = await _cartService.ApplyOrRemoveCouponAsync(cartViewModel);
        if (response is not null && response.IsSuccess)
        {
            TempData["Success"] = "Coupon applied successfully";
            return RedirectToAction(nameof(CartIndex));
        }
        return RedirectToAction("CartIndex");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveCoupon(CartViewModel cartViewModel)
    {
        cartViewModel.CartHeader.CouponCode = string.Empty;
        var response = await _cartService.ApplyOrRemoveCouponAsync(cartViewModel);
        if (response is not null && response.IsSuccess)
        {
            TempData["Success"] = "Coupon applied successfully";
            return RedirectToAction(nameof(CartIndex));
        }
        return RedirectToAction("CartIndex");
    }

    [HttpPost]
    public async Task<IActionResult> EmailCart(CartViewModel cartViewModel)
    {
        var cart = await LoadCartBasedOnLoggedInUser();
        cart.CartHeader.Email = User.FindFirstValue(ClaimTypes.Email);
        var response = await _cartService.EmailCart(cart);
        if (response is not null && response.IsSuccess)
        {
            TempData["Success"] = "Email will be processed and sent shortly.";
            return RedirectToAction(nameof(CartIndex));
        }
        return RedirectToAction("CartIndex");
    }

    private async Task<CartViewModel> LoadCartBasedOnLoggedInUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await _cartService.GetCartByUserIdAsync(userId!);
        if (response is not null && response.IsSuccess)
        {
            CartViewModel cartViewModel = JsonConvert.DeserializeObject<CartViewModel>(response.Result.ToString());
            return cartViewModel;
        }
        return new CartViewModel();
    }
}
