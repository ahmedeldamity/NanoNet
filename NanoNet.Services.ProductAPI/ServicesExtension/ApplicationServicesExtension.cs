using NanoNet.Services.ProductAPI.Helpers;
using NanoNet.Services.ProductAPI.Interfaces;
using NanoNet.Services.ProductAPI.Services;

namespace NanoNet.Services.ProductAPI.ServicesExtension;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfig));

        services.AddScoped<IProductService, ProductService>();

        return services;
    }

}