using NanoNet.Web.Utility;

namespace NanoNet.Web.ServicesExtension
{
    public static class ConfigurationClassesExtension
    {
        public static IServiceCollection ConfigureAppsettingData(this IServiceCollection services, IConfiguration configuration)
        {
            // Take setting data form appsetting to APIsUrl class
            services.Configure<APIsUrl>(configuration.GetSection("ServiceUrls"));

            return services;
        }
    }
}
