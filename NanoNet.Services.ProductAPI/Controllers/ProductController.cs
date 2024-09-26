using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNet.Services.ProductAPI.Dtos;
using NanoNet.Services.ProductAPI.ErrorHandling;
using NanoNet.Services.ProductAPI.Interfaces;

namespace NanoNet.Services.ProductAPI.Controllers;
public class ProductController(IProductService productService) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<Result>> GetAllProducts()
    {
        var result = await productService.GetProductsAsync();

        return result;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result>> GetProductById(int id)
    {
        var result = await productService.GetProductAsync(id);

        return result;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result>> AddProduct(ProductRequest productRequest)
    {
        var result = await productService.CreateProductAsync(productRequest);

        return result;
    }

    [HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result>> UpdateProduct(ProductRequest productRequest)
    {
        var result = await productService.UpdateProductAsync(productRequest);

        return result;
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result>> DeleteProduct(int id)
    {
        var result = await productService.DeleteProductAsync(id);

        return result;
    }
}