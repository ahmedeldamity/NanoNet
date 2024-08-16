﻿using NanoNet.Services.CouponAPI.Helpers;

namespace NanoNet.Services.ProductAPI.ServicesExtension
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
