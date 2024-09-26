namespace NanoNet.Services.ProductAPI.Dtos;
public class ProductRequest
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public string Description { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string? ImageUrl { get; set; } = null!;
}