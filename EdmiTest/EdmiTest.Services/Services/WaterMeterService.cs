using EdmiTest.Data.Interfaces;
using EdmiTest.Data.Models;
using EdmiTest.Data.Models.Responses;
using EdmiTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdmiTest.Services.Services
{
    public class WaterMeterService : IWaterMeterService
    {
        private IWaterMeterRepository _waterMeterRepository;

        public WaterMeterService(IWaterMeterRepository waterMeterRepository)
        {
            _waterMeterRepository = waterMeterRepository;
        }

        public async Task<AddDeviceResponse> Add(WaterMeter waterMeter)
        {
            try
            {
                await _waterMeterRepository.Add(waterMeter);
                return new AddWaterMeterResponse { Valid = true };
            }
            catch (Exception ex)
            {
                return new AddWaterMeterResponse { Valid = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<List<WaterMeter>> GetAll()
        {
            return await _waterMeterRepository.GetAll();
        }
    }
}
