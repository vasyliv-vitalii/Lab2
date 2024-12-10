using DomainLayer.Abstraction.IQueryRepositories;
using DomainLayer.Abstraction.IServices;
using DomainLayer.Models;

namespace BLLayer.Services
{
    public class BikeRouteService : IBikeRouteService
    {
        private readonly IBikeRouteQueryRepository _bikeRouteQueryRepository;

        public BikeRouteService(IBikeRouteQueryRepository bikeRouteQueryRepository)
        {
            _bikeRouteQueryRepository= bikeRouteQueryRepository;
        }

        public async Task<BikeRoute> CreateBikeRouteAsync(BikeRoute bikeRoute)
        {
            bikeRoute.Locations = new List<string>();
            return bikeRoute;
        }

        public async Task<BikeRoute> UpdateBikeRoute(int bikeRouteId, BikeRoute updatedBikeRoute)
        {
            var bikeRoute = await _bikeRouteQueryRepository.GetBikeRouteById(bikeRouteId);
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

        public async Task<BikeRoute> DeleteBikeRoute(int bikeRouteId)
        {
            var bikeRoute = await _bikeRouteQueryRepository.GetBikeRouteById(bikeRouteId);
            if (bikeRoute == null)
            {
                throw new Exception("FishngSpot not found");
            }

            return bikeRoute;
        }
    }
}
