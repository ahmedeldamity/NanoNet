using NanoNet.Services.CouponAPI.Helpers;
using NanoNet.Services.CouponAPI.Interfaces;
using NanoNet.Services.CouponAPI.Services;

namespace NanoNet.Services.CouponAPI.ServicesExtension;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfig));

        services.AddScoped<ICouponService, CouponService>();

        return services;
    }
}