using EdmiTest.Data.Models;
using EdmiTest.Data.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdmiTest.Data.Interfaces
{
    public interface IDeviceRepository<Device>
    {
        Task<bool> Add(Device device);
        Task<List<Device>> GetAll();
    }
}
