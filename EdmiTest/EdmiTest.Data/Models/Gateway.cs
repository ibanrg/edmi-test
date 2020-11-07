using System;
using System.Collections.Generic;
using System.Text;

namespace EdmiTest.Data.Models
{
    public class Gateway : Device
    {
        public string IP { get; set; }
        public int Port { get; set; }
    }
}