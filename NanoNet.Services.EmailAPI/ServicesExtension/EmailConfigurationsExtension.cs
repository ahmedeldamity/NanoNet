using Microsoft.EntityFrameworkCore;
using NanoNet.Services.EmailAPI.Data;

namespace NanoNet.Services.EmailAPI.ServicesExtension;
public static class EmailConfigurationsExtension
{
    public static IServiceCollection AddEmailConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EmailDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}