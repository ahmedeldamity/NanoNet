using Microsoft.EntityFrameworkCore;
using NanoNet.Services.OrderAPI.Models;

namespace NanoNet.Services.OrderAPI.Data;
public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
}