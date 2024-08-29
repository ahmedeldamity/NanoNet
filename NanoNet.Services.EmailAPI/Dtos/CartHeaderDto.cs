namespace NanoNet.Services.EmailAPI.Dtos;
public class CartHeaderDto
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    public double Discount { get; set; }
    public double TotalPrice { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}