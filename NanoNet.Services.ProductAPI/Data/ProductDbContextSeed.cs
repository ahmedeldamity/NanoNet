using NanoNet.Services.ProductAPI.Models;

namespace NanoNet.Services.ProductAPI.Data;
public class ProductDbContextSeed
{
    public static async Task SeedProductDataAsync(ProductDbContext _productContext)
    {
        if (!_productContext.Products.Any())
        {
            var products = new List<Product> {
                new() 
                {
                    Name = "Samosa",
                    Price = 15,
                    Description = "Risque vel lacks ac magna, vehicular sagittal ut non lacks.<br/> Vestibule arc turps, maximus dalesman deque. Phallus commode curses premium.",
                    ImageUrl = "https://placehold.co/603x403",
                    CategoryName = "Appetizer"
                },
                new()
                {
                    Name = "Pane Tika",
                    Price = 13.99,
                    Description = "Risque vel lacks ac magna, vehicular sagittal ut non lacks.<br/> Vestibule arc turps, maximus dalesman deque. Phallus commode curses premium.",
                    ImageUrl = "https://placehold.co/602x402",
                    CategoryName = "Appetizer"
                },
                new()
                {
                    Name = "Sweet Pie",
                    Price = 10.99,
                    Description = "Risque vel lacks ac magna, vehicular sagittal ut non lacks.<br/> Vestibule arc turps, maximus dalesman deque. Phallus commode curses premium.",
                    ImageUrl = "https://placehold.co/601x401",
                    CategoryName = "Dessert"
                },
                new()
                {
                    Name = "Pav Bhaji",
                    Price = 15,
                    Description = "Risque vel lacks ac magna, vehicular sagittal ut non lacks.<br/> Vestibule arc turps, maximus dalesman deque. Phallus commode curses premium.",
                    ImageUrl = "https://placehold.co/600x400",
                    CategoryName = "Entree"
                }
            };

            if (products?.Count > 0)
            {
                foreach (var product in products)
                {
                    _productContext.Products.Add(product);
                }
            }
        }

        await _productContext.SaveChangesAsync();
    }
}