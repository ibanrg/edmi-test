using EdmiTest.Data.Models;
using EdmiTest.Data.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdmiTest.Services.Interfaces
{
    public interface IDeviceService<Device>
    {
        Task<AddDeviceResponse> Add(Device device);
        Task<List<Device>> GetAll();
        Task<bool> Exists(string serialNumber);
    }
}
