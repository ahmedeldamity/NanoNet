using Microsoft.EntityFrameworkCore;
using NanoNet.Services.EmailAPI.Data;
using NanoNet.Services.EmailAPI.Services;

namespace NanoNet.Services.EmailAPI.ServicesExtension;
public static class EmailConfigurationsExtension
{
    public static IServiceCollection AddEmailConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Email Service with scope lifetime
        services.AddDbContext<EmailDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        // Add Email Service with singleton lifetime
        var optionBuilder = new DbContextOptionsBuilder<EmailDbContext>();

        optionBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        services.AddSingleton(new EmailService(optionBuilder.Options));

        return services;
    }
}