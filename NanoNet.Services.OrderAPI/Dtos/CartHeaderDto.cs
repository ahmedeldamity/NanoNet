namespace NanoNet.Services.OrderAPI.Dtos;
public class CartHeaderDto
{
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    public double Discount { get; set; }
    public double TotalPrice { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}