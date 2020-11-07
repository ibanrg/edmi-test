using System;
using System.Collections.Generic;
using System.Text;

namespace EdmiTest.Data.Models.Requests
{
    class AddGatewayRequest : AddDeviceRequest
    {
        public string IP { get; set; }
        public int Port { get; set; }
    }
}
