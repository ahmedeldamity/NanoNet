using NanoNet.Services.ShoppingCartAPI.Helpers;

namespace NanoNet.Services.ShoppingCartAPI.ServicesExtension
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig));

            return services;
        }
    }
}
