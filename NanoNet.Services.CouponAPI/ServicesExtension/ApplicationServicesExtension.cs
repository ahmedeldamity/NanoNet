using NanoNet.Services.CouponAPI.Helpers;
using NanoNet.Services.CouponAPI.Interfaces;

namespace NanoNet.Services.CouponAPI.ServicesExtension;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfig));

        services.AddScoped<ICouponService, ICouponService>();

        return services;
    }
}