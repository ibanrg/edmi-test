using EdmiTest.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace EdmiTest.Data.Models
{
    public class Device
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }
        public EState State { get; set; }
    }
}