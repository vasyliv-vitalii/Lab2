using FishingAndCyclingApp.Data;
using FishingAndCyclingApp.Models;

namespace FishingAndCyclingApp.Repositories
{
    public class RouteRepository : Repository<Models.Route>, IRouteRepository
    {
        public RouteRepository(AppDbContext context) : base(context) { }

       
    }
}
