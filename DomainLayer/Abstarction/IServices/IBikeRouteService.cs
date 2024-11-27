using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Abstarction.IServices
{
    public  interface IBikeRouteService
    {
        public Task<BikeRoute> CreateBikeRouteAsync(BikeRoute BikeRoute);
        public Task<BikeRoute> UpdateBikeRoute(int BikeRouteId, BikeRoute updatedBikeRoute);
        public Task<BikeRoute> DeleteBikeRoute(int BikeRouteId);
    }
}
