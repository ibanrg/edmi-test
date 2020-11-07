using System;
using System.Collections.Generic;
using System.Text;

namespace EdmiTest.Data.Models.Requests
{
    public class AddDeviceRequest
    {
        public int SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }
        public string State { get; set; }
    }
}
