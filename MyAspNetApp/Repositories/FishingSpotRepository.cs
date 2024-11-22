using FishingAndCyclingApp.Data;
using FishingAndCyclingApp.Models;

namespace FishingAndCyclingApp.Repositories
{
    public class FishingSpotRepository : Repository<FishingSpot>, IFishingSpotRepository
    {
        public FishingSpotRepository(AppDbContext context) : base(context) { }
    }
}
