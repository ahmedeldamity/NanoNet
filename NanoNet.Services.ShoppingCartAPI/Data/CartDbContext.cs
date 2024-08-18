using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ShoppingCartAPI.Models;

namespace NanoNet.Services.ShoppingCartAPI.Data
{
    public class CartDbContext: DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options): base(options) { }

        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
