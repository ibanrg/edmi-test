using EdmiTest.Data.Models;
using EdmiTest.Services.Interfaces;
using EdmiTest.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdmiTest.Services
{
    public static class RegisterServices
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IDeviceService<Device>, DeviceService>();
            services.AddScoped<IElectricMeterService, ElectricMeterService>();
            services.AddScoped<IWaterMeterService, WaterMeterService>();
            services.AddScoped<IGatewayService, GatewayService>();

            return services;
        }
    }
}
