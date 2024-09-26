using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NanoNet.Services.AuthAPI.Data;
using NanoNet.Services.AuthAPI.Models;

namespace NanoNet.Services.AuthAPI.ServicesExtension;
public static class IdentityConfigurationsExtension
{
	public static IServiceCollection AddIdentityConfigurations(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<IdentityContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
		});

		services.AddIdentity<AppUser, IdentityRole>(option =>
		{
			option.Password.RequireLowercase = true;
			option.Password.RequireUppercase = false;
			option.Password.RequireDigit = false;
			option.Password.RequireNonAlphanumeric = true;
			option.Password.RequiredUniqueChars = 3;
			option.Password.RequiredLength = 6;
		}).AddEntityFrameworkStores<IdentityContext>();

		return services;
	}
}