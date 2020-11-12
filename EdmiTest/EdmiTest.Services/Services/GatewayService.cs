using EdmiTest.Data.Enums;
using EdmiTest.Data.Interfaces;
using EdmiTest.Data.Models;
using EdmiTest.Data.Models.Responses;
using EdmiTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<BaseResponse> Add(Gateway gateway)
        {
            try
            {
                await _gatewayRepository.Add(gateway);
                return new BaseResponse { Valid = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Valid = false, ErrorMessages = new List<string> { ex.Message } };
            }
        }

        public async Task<BaseResponse> Exists(string serialNumber)
        {
            BaseResponse result = new BaseResponse();
            bool exists = await _gatewayRepository.Exists(serialNumber);
            if (exists)
            {
                result.Valid = false;
                result.ErrorMessages = new List<string> { "Serial Number already exists" };
            }
            return result;
        }

        public async Task<List<Gateway>> GetAll()
        {
            return await _gatewayRepository.GetAll();
        }

        public BaseResponse Validate(Gateway gateway)
        {
            BaseResponse result = new BaseResponse();

            // Serial number
            if (string.IsNullOrEmpty(gateway.SerialNumber)) result.ErrorMessages.Add("Serial Number is required");
            if (!string.IsNullOrEmpty(gateway.SerialNumber) && gateway.SerialNumber.Length < 4) result.ErrorMessages.Add("Serial Number min length is 4");
            if (!string.IsNullOrEmpty(gateway.SerialNumber) && gateway.SerialNumber.Length > 50) result.ErrorMessages.Add("Serial Number max length is 50");

            // Firmware Version
            if (!string.IsNullOrEmpty(gateway.FirmwareVersion) && !new Regex(@"^(\d+\.){1}(\d+)$").IsMatch(gateway.FirmwareVersion)) result.ErrorMessages.Add("Firmware Version format is not valid (xx.xx)");

            // State
            if (!Enum.IsDefined(typeof(EState), gateway.State)) result.ErrorMessages.Add("State value is not valid");

            // IP
            if (!string.IsNullOrEmpty(gateway.IP) && !new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b").IsMatch(gateway.IP)) result.ErrorMessages.Add("IP format is not valid (xxx.xxx.xxx.xxx)");

            // Port
            if (gateway.Port <= 0) result.ErrorMessages.Add("Port value must be greater than 0");
            if (gateway.Port > 65535) result.ErrorMessages.Add("Port value must be lower than 65535");

            result.Valid = result.ErrorMessages.Count == 0;
            return result;
        }
    }
}
