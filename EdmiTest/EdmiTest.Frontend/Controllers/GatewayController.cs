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
    public class GatewayController : Controller
    {
        IDeviceService<Device> _deviceService;
        IGatewayService _gatewayService;

        public GatewayController(IDeviceService<Device> deviceService, IGatewayService gatewayService)
        {
            _deviceService = deviceService;
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
            BaseResponse result;
            if (gateway == null)
            {
                return BadRequest();
            }

            result = await _deviceService.Exists(gateway.SerialNumber);
            if (!result.Valid)
            {
                return BadRequest(result);
            }

            result = _gatewayService.Validate(gateway);
            if (!result.Valid)
            {
                return BadRequest(result);
            }

            result = await _gatewayService.Add(gateway);
            return Ok(result);
        }
    }
}
