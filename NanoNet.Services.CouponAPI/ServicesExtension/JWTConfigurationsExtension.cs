using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NanoNet.Services.CouponAPI.SettingData;
using System.Text;

namespace NanoNet.Services.CouponAPI.ServicesExtension;
public static class JWTConfigurationsExtension
{
    public static IServiceCollection AddJWTConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var jwtData = serviceProvider.GetRequiredService<IOptions<JWTData>>().Value;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = jwtData.ValidAudience,
                ValidateIssuer = true,
                ValidIssuer = jwtData.ValidIssuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtData.SecretKey)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(jwtData.DurationInMinutes),
            };
        });

        return services;
    }
}