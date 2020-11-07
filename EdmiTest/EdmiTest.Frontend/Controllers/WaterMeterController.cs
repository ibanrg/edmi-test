using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdmiTest.Data.Models;
using EdmiTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EdmiTest.Frontend.Controllers
{
    [Route("api/[controller]")]
    public class WaterMeterController : Controller
    {
        IWaterMeterService _waterMeterService;

        public WaterMeterController(IWaterMeterService waterMeterService)
        {
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
            var result = await _waterMeterService.Add(waterMeter);
            return Ok(result);
        }
    }
}
