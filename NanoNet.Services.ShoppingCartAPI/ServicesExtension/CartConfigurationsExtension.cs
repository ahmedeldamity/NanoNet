using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ShoppingCartAPI.Data;

namespace NanoNet.Services.ShoppingCartAPI.ServicesExtension;
public static class CartConfigurationsExtension
{
    public static IServiceCollection AddShoppingCartConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CartDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}