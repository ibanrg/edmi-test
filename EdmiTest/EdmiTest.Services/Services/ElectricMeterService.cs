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
    public class ElectricMeterService : IElectricMeterService
    {
        private IElectricMeterRepository _electricMeterRepository;

        public ElectricMeterService(IElectricMeterRepository electricMeterRepository)
        {
            _electricMeterRepository = electricMeterRepository;
        }

        public async Task<AddDeviceResponse> Add(ElectricMeter electricMeter)
        {
            try
            {
                await _electricMeterRepository.Add(electricMeter);
                return new AddElectricMeterResponse { Valid = true };
            }
            catch (Exception ex)
            {
                return new AddElectricMeterResponse { Valid = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<List<ElectricMeter>> GetAll()
        {
            return await _electricMeterRepository.GetAll();
        }
    }
}
