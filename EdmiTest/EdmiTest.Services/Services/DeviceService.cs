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

        public Task<BaseResponse> Add(Device device)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> Exists(string serialNumber)
        {
            BaseResponse result;
            result = await _electricMeterService.Exists(serialNumber);
            if (result.Valid)
            {
                result = await _waterMeterService.Exists(serialNumber);
                if (result.Valid)
                {
                    result = await _gatewayService.Exists(serialNumber);
                }
            }
            return result;
        }

        public Task<List<Device>> GetAll()
        {
            throw new NotImplementedException();
        }

        public BaseResponse Validate(Device device)
        {
            throw new NotImplementedException();
        }
    }
}
