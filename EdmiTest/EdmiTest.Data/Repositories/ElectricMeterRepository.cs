using EdmiTest.Data.Contexts;
using EdmiTest.Data.Interfaces;
using EdmiTest.Data.Models;
using EdmiTest.Data.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            try
            {
                await _db.ElectricMeters.AddAsync(electricMeter);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var a = ex;
                return false;
            }
        }

        public async Task<List<ElectricMeter>> GetAll()
        {
            return await _db.ElectricMeters.ToListAsync();
        }
    }
}
