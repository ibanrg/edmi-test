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
        Task<BaseResponse> Add(Device device);
        Task<List<Device>> GetAll();
        Task<BaseResponse> Exists(string serialNumber);
        BaseResponse Validate(Device device);
    }
}
