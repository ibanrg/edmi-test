using EdmiTest.Data.Contexts;
using EdmiTest.Data.Interfaces;
using EdmiTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdmiTest.Data.Repositories
{
    public class ElectricMeterRepository : IElectricMeterRepository
    {
        private readonly EdmiDbContext _db;

        public ElectricMeterRepository(EdmiDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Add(ElectricMeter electricMeter)
        {
            await _db.ElectricMeters.AddAsync(electricMeter);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(string serialNumber)
        {
            return await _db.ElectricMeters.AnyAsync(x => x.SerialNumber == serialNumber);
        }

        public async Task<List<ElectricMeter>> GetAll()
        {
            return await _db.ElectricMeters.ToListAsync();
        }
    }
}
