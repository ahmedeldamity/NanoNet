using Microsoft.EntityFrameworkCore;
using NanoNet.Services.CouponAPI.Models;

namespace NanoNet.Services.CouponAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
