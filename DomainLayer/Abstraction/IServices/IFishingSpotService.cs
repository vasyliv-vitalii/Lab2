using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Abstraction.IServices
{
    public interface IFishingSpotService
    {
        public Task<FishingSpot> CreateFishingSpotAsync(FishingSpot fishingSpot);
        public Task<FishingSpot> UpdateFishingSpot(int fishingSpotd, FishingSpot updatedFishingSpot);
        public Task<FishingSpot> DeleteFishingSpot(int fishingSpotId);
    }
}
