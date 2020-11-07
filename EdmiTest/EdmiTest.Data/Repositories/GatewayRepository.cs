using EdmiTest.Data.Contexts;
using EdmiTest.Data.Interfaces;
using EdmiTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdmiTest.Data.Repositories
{
    public class GatewayRepository : IGatewayRepository
    {
        private readonly EdmiDbContext _db;

        public GatewayRepository(EdmiDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Add(Gateway gateway)
        {
            await _db.Gateways.AddAsync(gateway);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(string serialNumber)
        {
            return await _db.Gateways.AnyAsync(x => x.SerialNumber == serialNumber);
        }

        public async Task<List<Gateway>> GetAll()
        {
            return await _db.Gateways.ToListAsync();
        }
    }
}
