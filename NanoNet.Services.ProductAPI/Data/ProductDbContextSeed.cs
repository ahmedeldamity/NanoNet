using NanoNet.Services.ProductAPI.Models;

namespace NanoNet.Services.ProductAPI.Data
{
    public class ProductDbContextSeed
    {
        public async static Task SeedProductDataAsync(ProductDbContext _productContext)
        {
            if (_productContext.Products.Count() == 0)
            {
                var products = new List<Product> {
                    new Product {
                        Name = "Samosa",
                        Price = 15,
                        Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                        ImageUrl = "https://placehold.co/603x403",
                        CategoryName = "Appetizer"
                    },
                    new Product
                    {
                        Name = "Paneer Tikka",
                        Price = 13.99,
                        Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                        ImageUrl = "https://placehold.co/602x402",
                        CategoryName = "Appetizer"
                    },
                    new Product
                    {
                        Name = "Sweet Pie",
                        Price = 10.99,
                        Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                        ImageUrl = "https://placehold.co/601x401",
                        CategoryName = "Dessert"
                    },
                    new Product
                    {
                        Name = "Pav Bhaji",
                        Price = 15,
                        Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                        ImageUrl = "https://placehold.co/600x400",
                        CategoryName = "Entree"
                    }
                };

                if (products?.Count() > 0)
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
}
