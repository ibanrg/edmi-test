using EdmiTest.Data.Contexts;
using EdmiTest.Data.Interfaces;
using EdmiTest.Data.Models;
using EdmiTest.Data.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
            return true;
        }

        public async Task<List<Gateway>> GetAll()
        {
            return await _db.Gateways.ToListAsync();
        }
    }
}
