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
    public class GatewayService : IGatewayService
    {
        private IGatewayRepository _gatewayRepository;

        public GatewayService(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
        }

        public async Task<AddDeviceResponse> Add(Gateway gateway)
        {
            try
            {
                await _gatewayRepository.Add(gateway);
                return new AddGatewayResponse { Valid = true };
            }
            catch (Exception ex)
            {
                return new AddGatewayResponse { Valid = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<List<Gateway>> GetAll()
        {
            return await _gatewayRepository.GetAll();
        }
    }
}
