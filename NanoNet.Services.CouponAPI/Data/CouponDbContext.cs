using Microsoft.EntityFrameworkCore;
using NanoNet.Services.CouponAPI.Models;

namespace NanoNet.Services.CouponAPI.Data
{
    public class CouponDbContext: DbContext
    {
        public CouponDbContext(DbContextOptions<CouponDbContext> options): base(options) { }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
