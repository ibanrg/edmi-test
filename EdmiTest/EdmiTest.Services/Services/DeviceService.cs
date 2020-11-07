using EdmiTest.Data.Models;
using EdmiTest.Data.Models.Responses;
using EdmiTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdmiTest.Services.Services
{
    class DeviceService : IDeviceService<Device>
    {
        IElectricMeterService _electricMeterService;
        IWaterMeterService _waterMeterService;
        IGatewayService _gatewayService;

        public DeviceService(IElectricMeterService electricMeterService, IWaterMeterService waterMeterService, IGatewayService gatewayService)
        {
            _electricMeterService = electricMeterService;
            _waterMeterService = waterMeterService;
            _gatewayService = gatewayService;
        }

        public Task<AddDeviceResponse> Add(Device device)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exists(string serialNumber)
        {
            return await _electricMeterService.Exists(serialNumber) || await _waterMeterService.Exists(serialNumber) || await _gatewayService.Exists(serialNumber);
        }

        public Task<List<Device>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
