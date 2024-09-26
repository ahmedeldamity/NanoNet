
using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ProductAPI.Models;

namespace NanoNet.Services.ProductAPI.Data;
public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
    }

    public DbSet<Product> Products { get; set; }
}