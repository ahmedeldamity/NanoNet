namespace NanoNet.Services.ProductAPI.Models;
public class Product: BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double Price { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}