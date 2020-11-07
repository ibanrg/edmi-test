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
    public class GatewayController : Controller
    {
        IGatewayService _gatewayService;

        public GatewayController(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _gatewayService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Gateway gateway)
        {
            var result = await _gatewayService.Add(gateway);
            return Ok(result);
        }
    }
}
