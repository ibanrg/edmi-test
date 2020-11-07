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
    public class WaterMeterController : Controller
    {
        IDeviceService<Device> _deviceService;
        IWaterMeterService _waterMeterService;

        public WaterMeterController(IDeviceService<Device> deviceService, IWaterMeterService waterMeterService)
        {
            _deviceService = deviceService;
            _waterMeterService = waterMeterService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _waterMeterService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] WaterMeter waterMeter)
        {
            if (await _deviceService.Exists(waterMeter.SerialNumber))
            {
                return BadRequest(new AddDeviceResponse { Valid = false, ErrorMessage = "A device with that Serial Number already exists." });
            }
            await _waterMeterService.Add(waterMeter);
            return Ok(new AddDeviceResponse());
        }
    }
}
