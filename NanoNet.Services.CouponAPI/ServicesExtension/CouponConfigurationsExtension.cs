using Microsoft.EntityFrameworkCore;
using NanoNet.Services.CouponAPI.Data;

namespace NanoNet.Services.CouponAPI.ServicesExtension;
public static class CouponConfigurationsExtension
{
	public static IServiceCollection AddCouponConfigurations(this IServiceCollection services, IConfiguration configuration)
	{
        services.AddDbContext<CouponDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
	}
}