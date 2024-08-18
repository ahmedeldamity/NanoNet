using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ShoppingCartAPI.Data;

namespace NanoNet.Services.ShoppingCartAPI.ServicesExtension
{
    public static class ShoppingCartConfigurationsExtension
    {
        public static IServiceCollection AddShoppingCartConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShoppingDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
