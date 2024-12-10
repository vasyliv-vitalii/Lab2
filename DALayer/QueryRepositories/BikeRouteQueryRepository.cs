using DALayer.DataBase;
using DomainLayer.Abstraction.IQueryRepositories;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALayer.QueryRepositories
{
    public class BikeRouteQueryRepository : IBikeRouteQueryRepository
    {
        private readonly AppDbContext _context;
        public BikeRouteQueryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<BikeRoute>> GetAllBikeRoutes()
        {
            return await _context.BikeRoutes.ToListAsync();
        }

        public async Task<BikeRoute?> GetBikeRouteById(int BikeRouteId)
        {
            return await _context.BikeRoutes.FirstOrDefaultAsync(x => x.Id == BikeRouteId);
        }
    }
}
