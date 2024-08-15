
using Microsoft.EntityFrameworkCore;

namespace NanoNet.Services.ProductAPI.Data
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options): base(options) { }

    }
}
