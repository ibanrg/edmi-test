using EdmiTest.Data.Interfaces;
using EdmiTest.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdmiTest.Services
{
    public static class RegisterDataServices
    {
        public static IServiceCollection AddCustomDataServices(this IServiceCollection services)
        {
            services.AddScoped<IElectricMeterRepository, ElectricMeterRepository>();
            services.AddScoped<IWaterMeterRepository, WaterMeterRepository>();
            services.AddScoped<IGatewayRepository, GatewayRepository>();

            return services;
        }
    }
}
