using DomainLayer.Models;


namespace DomainLayer.Abstraction.IServices
{
    public  interface IBikeRouteService
    {
        public Task<BikeRoute> CreateBikeRouteAsync(BikeRoute bikeRoute);
        public Task<BikeRoute> UpdateBikeRoute(int bikeRouteId, BikeRoute updatedBikeRoute);
        public Task<BikeRoute> DeleteBikeRoute(int bikeRouteId);
    }
}
