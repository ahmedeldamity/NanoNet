using Microsoft.AspNetCore.Mvc;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;
using Newtonsoft.Json;

namespace NanoNet.Web.Controllers
{
    public class ProductController(IProductService _productService) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> ProductIndex()
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
    }
}
