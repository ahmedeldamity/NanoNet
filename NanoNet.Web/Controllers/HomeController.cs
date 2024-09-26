using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;
using Newtonsoft.Json;
using System.Security.Claims;

namespace NanoNet.Web.Controllers;
public class HomeController(IProductService _productService, ICartService _cartService) : Controller
{

    [ActionName("Index")]
    public async Task<IActionResult> Index()
    {
        List<ProductViewModel>? list = [];

        var response = await _productService.GetAllProductsAsync();

        if (response?.IsSuccess is true)
        {
            var jsonData = Convert.ToString(response.Value);

            if (string.IsNullOrEmpty(jsonData))
            {
                TempData["error"] = "No products found";

                return View(list);
            }

            list = JsonConvert.DeserializeObject<List<ProductViewModel>>(jsonData);
        }

        TempData["error"] = response?.Error;

        return View(list);
    }

    [Authorize]
    public async Task<IActionResult> ProductDetails(int productId)
    {
        ProductViewModel? model = new();

        var response = await _productService.GetProductByIdAsync(productId);

        if (response is not null && response.IsSuccess)
        {
            var jsonData = Convert.ToString(response.Value);

            if (string.IsNullOrEmpty(jsonData))
            {
                TempData["error"] = "Product not found";
                return View(model);
            }

            model = JsonConvert.DeserializeObject<ProductViewModel>(jsonData);
        }

        TempData["error"] = response?.Error;

        return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ProductDetails(ProductViewModel productViewModel)
    {
        CartViewModel? cartItem = new()
        {
            CartHeader = new CartHeaderViewModel
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            },
            CartItems = new List<CartItemViewModel>
            {
                new()
                {
                    ProductId = productViewModel.Id,
                    Count = productViewModel.Count
                }
            }
        };

        var response = await _cartService.UpsertCartAsync(cartItem);

        if (response is not null && response.IsSuccess)
        {
            TempData["success"] = "Product added to cart successfully";

            return RedirectToAction(nameof(Index));
        }

        TempData["error"] = response?.Error;

        return View(productViewModel);
    }

}