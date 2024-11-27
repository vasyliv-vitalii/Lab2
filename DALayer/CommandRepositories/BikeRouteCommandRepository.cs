using DALayer.DataBase;
using DomainLayer.Abstarction.ICommandRepositories;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALayer.CommandRepositories
{
    public class BikeRouteCommandRepository : IBikeRouteCommandRepository
    {
        private readonly AppDbContext _context;

        public BikeRouteCommandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BikeRoute> CreateBikeRoute(BikeRoute bikeRoute)
        {
            _context.BikeRoutes.Add(bikeRoute);
            await _context.SaveChangesAsync();
            return bikeRoute;
        }

        public async Task<BikeRoute> UpdateBikeRoute(BikeRoute bikeRoute)
        {
            _context.BikeRoutes.Remove(bikeRoute);
            await _context.SaveChangesAsync();
            return bikeRoute;
        }

        public async Task DeleteBikeRoute(BikeRoute bikeRoute)
        {
            _context.BikeRoutes.Remove(bikeRoute);
            await _context.SaveChangesAsync();
        }
    }
}
