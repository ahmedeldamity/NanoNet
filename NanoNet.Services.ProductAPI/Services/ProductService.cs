using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ProductAPI.Data;
using NanoNet.Services.ProductAPI.Dtos;
using NanoNet.Services.ProductAPI.ErrorHandling;
using NanoNet.Services.ProductAPI.Interfaces;
using NanoNet.Services.ProductAPI.Models;

namespace NanoNet.Services.ProductAPI.Services;
public class ProductService(ProductDbContext productDbContext, IMapper mapper): IProductService
{
    public async Task<Result<IReadOnlyList<ProductResponse>>> GetProductsAsync()
    {
        var products = await productDbContext.Products.ToListAsync();

        var productsResponse = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductResponse>>(products);

        return Result.Success(productsResponse);
    }

    public async Task<Result<ProductResponse>> GetProductAsync(int id)
    {
        var product = await productDbContext.Products.FindAsync(id);

        if (product is null)
            return Result.Failure<ProductResponse>("The product you are looking for does not exist. Please check the product ID and try again.");

        var productResponse = mapper.Map<Product, ProductResponse>(product);

        return Result.Success(productResponse);
    }

    public async Task<Result<ProductResponse>> CreateProductAsync(ProductRequest productRequest)
    {
        var product = mapper.Map<Product>(productRequest);

        if (product is null)
            return Result.Failure<ProductResponse>("The product you are trying to add is invalid. Please check the product details and try again.");

        await productDbContext.AddAsync(product);

        await productDbContext.SaveChangesAsync();

        var productResponse = mapper.Map<Product, ProductResponse>(product);

        return Result.Success(productResponse);
    }

    public async Task<Result<ProductResponse>> UpdateProductAsync(ProductRequest productRequest)
    {
        var product = mapper.Map<Product>(productRequest);

        if (product is null)
            return Result.Failure<ProductResponse>("The product you are trying to add is invalid. Please check the product details and try again.");

        productDbContext.Update(product);

        await productDbContext.SaveChangesAsync();

        var productResponse = mapper.Map<Product, ProductResponse>(product);

        return Result.Success(productResponse);
    }

    public async Task<Result<ProductResponse>> DeleteProductAsync(int id)
    {
        var product = await productDbContext.Products.FindAsync(id);

        if (product is null)
            return Result.Failure<ProductResponse>("The product you are looking for does not exist. Please check the product ID and try again.");

        productDbContext.Remove(product);

        await productDbContext.SaveChangesAsync();

        var productResponse = mapper.Map<Product, ProductResponse>(product);

        return Result.Success(productResponse);
    }
}