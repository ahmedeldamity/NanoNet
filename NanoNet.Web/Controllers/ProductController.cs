using Microsoft.AspNetCore.Mvc;
using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;
using Newtonsoft.Json;

namespace NanoNet.Web.Controllers;
public class ProductController(IProductService _productService) : Controller
{

    [HttpGet]
    public async Task<IActionResult> ProductIndex()
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

	public IActionResult ProductCreate()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> ProductCreate(ProductViewModel productModel)
	{
        if (ModelState.IsValid is false) 
            return View(productModel);

        var response = await _productService.CreateProductAsync(productModel);

        if (response is not null && response.IsSuccess)
        {
            TempData["success"] = "Product created successfully";

            return RedirectToAction(nameof(ProductIndex));
        }
        
        TempData["error"] = response?.Error;

        return View(productModel);
	}

    public async Task<IActionResult> ProductEdit(int productId)
    {
        var response = await _productService.GetProductByIdAsync(productId);

        if (response?.IsSuccess is true)
        {
            var jsonData = Convert.ToString(response.Value);

            if (string.IsNullOrEmpty(jsonData))
            {
                TempData["error"] = "Product not found";
                return View();
            }

            var model = JsonConvert.DeserializeObject<ProductViewModel>(jsonData);

            return View(model);
        }
        
        TempData["error"] = response?.Error;
        
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ProductEdit(ProductViewModel productDto)
    {
        if (ModelState.IsValid is false)
            return View(productDto);

        var response = await _productService.UpdateProductAsync(productDto);

        if (response?.IsSuccess is false)
        {
            TempData["success"] = "Product updated successfully";

            return RedirectToAction(nameof(ProductIndex));
        }
            
        TempData["error"] = response?.Error;
        
        return View(productDto);
    }

    public async Task<IActionResult> ProductDelete(int productId)
    {
        var response = await _productService.GetProductByIdAsync(productId);

        if (response is not null && response.IsSuccess)
        {
            var jsonData = Convert.ToString(response.Value);

            if (string.IsNullOrEmpty(jsonData))
            {
                TempData["error"] = "Product not found";
                return NotFound();
            }

            var model = JsonConvert.DeserializeObject<ProductViewModel>(jsonData);

            return View(model);
        }
            
        TempData["error"] = response?.Error;

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductViewModel productModel)
    {
        var response = await _productService.DeleteProductAsync(productModel.Id);

        if (response is not null && response.IsSuccess)
        {
            TempData["success"] = "Product deleted successfully";

            return RedirectToAction(nameof(ProductIndex));
        }
           
        TempData["error"] = response?.Error;

        return View(productModel);
    }
}