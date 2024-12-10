using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Abstraction.IQueryRepositories
{
    public interface IFishingSpotQueryRepository
    {
        public Task<List<FishingSpot>> GetAllFishingSpots();
        public Task<FishingSpot?> GetFishingSpotById(int FishingSpotId);
    }
}
