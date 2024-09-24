namespace NanoNet.Services.ShoppingCartAPI.Dtos;
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string? ImageUrl { get; set; }
}
