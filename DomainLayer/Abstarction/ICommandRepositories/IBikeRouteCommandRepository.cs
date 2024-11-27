using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Abstarction.ICommandRepositories
{
    public interface IBikeRouteCommandRepository
    {
        public Task<BikeRoute> CreateBikeRoute(BikeRoute bikeRoute);
        public Task<BikeRoute> UpdateBikeRoute(BikeRoute bikeRoute);
        public Task DeleteBikeRoute(BikeRoute bikeRoute);
    }
}
