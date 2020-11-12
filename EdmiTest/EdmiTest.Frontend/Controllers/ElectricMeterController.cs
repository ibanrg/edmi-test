using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdmiTest.Data.Models;
using EdmiTest.Data.Models.Responses;
using EdmiTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EdmiTest.Frontend.Controllers
{
    [Route("api/[controller]")]
    public class ElectricMeterController : Controller
    {
        IDeviceService<Device> _deviceService;
        IElectricMeterService _electricMeterService;

        public ElectricMeterController(IDeviceService<Device> deviceService, IElectricMeterService electricMeterService)
        {
            _deviceService = deviceService;
            _electricMeterService = electricMeterService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _electricMeterService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ElectricMeter electricMeter)
        {
            BaseResponse result;
            if (electricMeter == null)
            {
                return BadRequest();
            }

            result = await _deviceService.Exists(electricMeter.SerialNumber);
            if (!result.Valid)
            {
                return BadRequest(result);
            }

            result = _electricMeterService.Validate(electricMeter);
            if (!result.Valid)
            {
                return BadRequest(result);
            }

            result = await _electricMeterService.Add(electricMeter);
            return Ok(result);
        }
    }
}