using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Abstraction.IQueryRepositories
{
    public interface IBikeRouteQueryRepository
    {
        public Task<List<BikeRoute>> GetAllBikeRoutes();
        public Task<BikeRoute?> GetBikeRouteById(int BikeRouteId);
    }
}
