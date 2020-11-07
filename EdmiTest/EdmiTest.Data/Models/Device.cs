using EdmiTest.Data.Enums;
using System;

namespace EdmiTest.Data.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }
        public EState State { get; set; }
    }
}