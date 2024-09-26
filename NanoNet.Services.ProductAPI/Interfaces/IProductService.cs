using NanoNet.Services.ProductAPI.Dtos;
using NanoNet.Services.ProductAPI.ErrorHandling;
using NanoNet.Services.ProductAPI.Models;

namespace NanoNet.Services.ProductAPI.Interfaces;
public interface IProductService
{
    Task<Result<IReadOnlyList<ProductResponse>>> GetProductsAsync();
    Task<Result<ProductResponse>> GetProductAsync(int id);
    Task<Result<ProductResponse>> CreateProductAsync(ProductRequest productRequest);
    Task<Result<ProductResponse>> UpdateProductAsync(ProductRequest productRequest);
    Task<Result<ProductResponse>> DeleteProductAsync(int id);
}