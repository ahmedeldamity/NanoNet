using Microsoft.EntityFrameworkCore;

namespace NanoNet.Services.ShoppingCartAPI.Data
{
    public class ShoppingDbContext: DbContext
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options): base(options) { }
    }
}
