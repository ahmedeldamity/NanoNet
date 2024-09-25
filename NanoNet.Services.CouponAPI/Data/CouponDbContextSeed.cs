using NanoNet.Services.CouponAPI.Models;

namespace NanoNet.Services.CouponAPI.Data;
public class CouponDbContextSeed
{
    public static async Task SeedProductDataAsync(CouponDbContext _couponContext)
    {
        if (_couponContext.Coupons.Any() is false)
        {
            var coupons = new List<Coupon> {
                new() 
                {
                    CouponCode = "10OFF",
                    DiscountAmount = 10,
                    MinAmount = 20
                },
                new()
                {
                    CouponCode = "20OFF",
                    DiscountAmount = 20,
                    MinAmount = 40
                }
            };

            if (coupons?.Count > 0)
            {
                foreach (var coupon in coupons)
                {
                    _couponContext.Coupons.Add(coupon);
                }
            }
        }

        await _couponContext.SaveChangesAsync();
    }
}