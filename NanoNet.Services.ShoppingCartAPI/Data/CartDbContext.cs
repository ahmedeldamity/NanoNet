using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ShoppingCartAPI.Models;

namespace NanoNet.Services.ShoppingCartAPI.Data;
public class CartDbContext(DbContextOptions<CartDbContext> options) : DbContext(options)
{
    public DbSet<CartHeader> CartHeaders { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
}