using EdmiTest.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdmiTest.Data.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public int SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }
        public EState State { get; set; }
    }
}