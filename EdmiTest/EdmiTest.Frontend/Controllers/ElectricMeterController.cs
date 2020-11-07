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
    public class ElectricMeterController : Controller
    {
        IElectricMeterService _electricMeterService;

        public ElectricMeterController(IElectricMeterService electricMeterService)
        {
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
            var result = await _electricMeterService.Add(electricMeter);
            return Ok(result);
        }
    }
}
