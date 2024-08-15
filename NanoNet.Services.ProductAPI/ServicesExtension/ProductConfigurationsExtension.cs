using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ProductAPI.Data;

namespace NanoNet.Services.ProductAPI.ServicesExtension
{
    public static class ProductConfigurationsExtension
    {
        public static IServiceCollection AddProductConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
