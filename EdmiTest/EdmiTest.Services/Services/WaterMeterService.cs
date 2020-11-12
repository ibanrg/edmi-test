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
    public class WaterMeterService : IWaterMeterService
    {
        private IWaterMeterRepository _waterMeterRepository;

        public WaterMeterService(IWaterMeterRepository waterMeterRepository)
        {
            _waterMeterRepository = waterMeterRepository;
        }

        public async Task<BaseResponse> Add(WaterMeter waterMeter)
        {
            try
            {
                await _waterMeterRepository.Add(waterMeter);
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
            bool exists = await _waterMeterRepository.Exists(serialNumber);
            if (exists)
            {
                result.Valid = false;
                result.ErrorMessages = new List<string> { "Serial Number already exists" };
            }
            return result;
        }

        public async Task<List<WaterMeter>> GetAll()
        {
            return await _waterMeterRepository.GetAll();
        }

        public BaseResponse Validate(WaterMeter waterMeter)
        {
            BaseResponse result = new BaseResponse();

            // Serial number
            if (string.IsNullOrEmpty(waterMeter.SerialNumber)) result.ErrorMessages.Add("Serial Number is required");
            if (!string.IsNullOrEmpty(waterMeter.SerialNumber) && waterMeter.SerialNumber.Length < 4) result.ErrorMessages.Add("Serial Number min length is 4");
            if (!string.IsNullOrEmpty(waterMeter.SerialNumber) && waterMeter.SerialNumber.Length > 50) result.ErrorMessages.Add("Serial Number max length is 50");

            // Firmware Version
            if (!string.IsNullOrEmpty(waterMeter.FirmwareVersion) && !new Regex(@"^(\d+\.){1}(\d+)$").IsMatch(waterMeter.FirmwareVersion)) result.ErrorMessages.Add("Firmware Version format is not valid (xx.xx)");

            // State
            if (!Enum.IsDefined(typeof(EState), waterMeter.State)) result.ErrorMessages.Add("State value is not valid");

            result.Valid = result.ErrorMessages.Count == 0;
            return result;
        }
    }
}
