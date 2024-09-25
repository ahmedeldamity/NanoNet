namespace NanoNet.Services.CouponAPI.Models;
public class Coupon
{
    public int CouponId { get; set; }
    public string CouponCode { get; set; } = null!;
    public double DiscountAmount { get; set; }
    public int MinAmount { get; set; }
}