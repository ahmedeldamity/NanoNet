using NanoNet.Services.ProductAPI.Models;
using System.Text.Json;

namespace NanoNet.Services.ProductAPI.Data;
public class ProductDbContextSeed
{
    public static async Task SeedProductDataAsync(ProductDbContext _productContext, IConfiguration configuration)
    {
		if (_productContext.Products.Any() is false)
		{
			var productsFilePath = Path.Combine("DataSeeding", "products.json");

			var productsJsonData = await File.ReadAllTextAsync(productsFilePath);

			var products = JsonSerializer.Deserialize<List<Product>>(productsJsonData);

			var baseUrl = configuration["BaseUrl"];

			if (products?.Count > 0)
			{
				foreach (var product in products)
				{
					product.ImageUrl = $"{baseUrl}{product.ImageUrl}";
					_productContext.Products.Add(product);
				}
			}
		}

		await _productContext.SaveChangesAsync();
	}
}