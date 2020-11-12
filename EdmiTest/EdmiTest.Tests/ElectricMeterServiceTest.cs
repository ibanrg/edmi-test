using EdmiTest.Data.Contexts;
using EdmiTest.Data.Interfaces;
using EdmiTest.Data.Models;
using EdmiTest.Data.Repositories;
using EdmiTest.Services.Interfaces;
using EdmiTest.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace EdmiTest.Tests
{
    public class ElectricMeterServiceTest
    {
        private ServiceProvider serviceProvider { get; set; }

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddTransient<IElectricMeterService, ElectricMeterService>();
            services.AddTransient<IElectricMeterRepository, ElectricMeterRepository>();
            services.AddDbContext<EdmiDbContext>();
            serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void Validate_SerialNumberNull_ReturnNotValid()
        {
            var service = serviceProvider.GetService<IElectricMeterService>();
            var result = service.Validate(new ElectricMeter());
            Assert.False(result.Valid, "Serial Number should not be null");
        }

        [Test]
        public void Validate_FirmwareVersionInvalidFormat_ReturnNotValid()
        {
            var service = serviceProvider.GetService<IElectricMeterService>();
            var result = service.Validate(new ElectricMeter()
            {
                SerialNumber = "XXXXXXXXXXXX",
                FirmwareVersion = "5321.5."
            });
            Assert.False(result.Valid, "Serial Number should not be null");
        }
    }
}

