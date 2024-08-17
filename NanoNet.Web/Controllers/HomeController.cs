using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;
using Newtonsoft.Json;

namespace NanoNet.Web.Controllers
{
    public class HomeController(IProductService _productService) : Controller
    {

        public async Task<IActionResult> Index()
        {
            List<ProductViewModel>? list = new();

            ResponseViewModel? response = await _productService.GetAllProductsAsync();

            if (response is not null && response.IsSuccess)
            {
                var jsonData = Convert.ToString(response.Result);
                list = JsonConvert.DeserializeObject<List<ProductViewModel>>(jsonData);
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductViewModel? model = new();

            ResponseViewModel? response = await _productService.GetProductByIdAsync(productId);

            if (response is not null && response.IsSuccess)
            {
                var jsonData = Convert.ToString(response.Result);
                model = JsonConvert.DeserializeObject<ProductViewModel>(jsonData);
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(model);
        }
    }
}
