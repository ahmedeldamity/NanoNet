using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ShoppingCartAPI.Models;

namespace NanoNet.Services.ShoppingCartAPI.Data
{
    public class ShoppingDbContext: DbContext
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options): base(options) { }

        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
    }
}
