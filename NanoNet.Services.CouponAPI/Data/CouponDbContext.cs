using Microsoft.EntityFrameworkCore;
using NanoNet.Services.CouponAPI.Models;

namespace NanoNet.Services.CouponAPI.Data;
public class CouponDbContext(DbContextOptions<CouponDbContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; }
}