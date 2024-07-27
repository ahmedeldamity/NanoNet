using NanoNet.Services.CouponAPI.Models;

namespace NanoNet.Services.CouponAPI.Data
{
    public class CouponDbContextSeed
    {
        public async static Task SeedProductDataAsync(CouponDbContext _couponContext)
        {
            if (_couponContext.Coupons.Count() == 0)
            {
                var coupons = new List<Coupon> {
                    new Coupon {
                        CouponCode = "10OFF",
                        DiscountAmount = 10,
                        MinAmount = 20
                    },
                    new Coupon {
                        CouponCode = "20OFF",
                        DiscountAmount = 20,
                        MinAmount = 40
                    }
                };

                if (coupons?.Count() > 0)
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
}
