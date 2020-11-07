using EdmiTest.Data.Contexts;
using EdmiTest.Data.Interfaces;
using EdmiTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdmiTest.Data.Repositories
{
    public class WaterMeterRepository : IWaterMeterRepository
    {
        private readonly EdmiDbContext _db;

        public WaterMeterRepository(EdmiDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Add(WaterMeter WaterMeter)
        {
            await _db.WaterMeters.AddAsync(WaterMeter);
            return true;
        }

        public async Task<List<WaterMeter>> GetAll()
        {
            return await _db.WaterMeters.ToListAsync();
        }
    }
}
