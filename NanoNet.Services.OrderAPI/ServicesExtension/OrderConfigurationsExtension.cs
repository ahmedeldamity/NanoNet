using Microsoft.EntityFrameworkCore;
using NanoNet.Services.OrderAPI.Data;

namespace NanoNet.Services.ShoppingCartAPI.ServicesExtension
{
    public static class OrderConfigurationsExtension
    {
        public static IServiceCollection AddOrderConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
