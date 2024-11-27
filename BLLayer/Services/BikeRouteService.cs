using DomainLayer.Abstarction.IQueryRepositories;
using DomainLayer.Abstarction.IServices;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Services
{
    public class BikeRouteService : IBikeRouteService
    {
        private readonly IBikeRouteQueryRepository _bikeRouteQueryRepository;

        public BikeRouteService(IBikeRouteQueryRepository bikeRouteQueryRepository)
        {
            _bikeRouteQueryRepository= bikeRouteQueryRepository;
        }

        public async Task<BikeRoute> CreateBikeRouteAsync(BikeRoute BikeRoute)
        {
            BikeRoute.Locations = new List<string>();
            return BikeRoute;
        }

        public async Task<BikeRoute> UpdateBikeRoute(int BikeRouteId, BikeRoute updatedBikeRoute)
        {
            var bikeRoute = await _bikeRouteQueryRepository.GetBikeRouteById(BikeRouteId);
            if (bikeRoute == null)
            {
                throw new Exception("fishingSpot not found");
            }
            bikeRoute.Name = updatedBikeRoute.Name;
            bikeRoute.Description = updatedBikeRoute.Description;
            bikeRoute.Distance = updatedBikeRoute.Distance;
            bikeRoute.Difficulty = updatedBikeRoute.Difficulty;
            bikeRoute.Locations = updatedBikeRoute.Locations;
            return bikeRoute;
        }

        public async Task<BikeRoute> DeleteBikeRoute(int BikeRouteId)
        {
            var bikeRoute = await _bikeRouteQueryRepository.GetBikeRouteById(BikeRouteId);
            if (bikeRoute == null)
            {
                throw new Exception("FishngSpot not found");
            }

            return bikeRoute;
        }
    }
}
