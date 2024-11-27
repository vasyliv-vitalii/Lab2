using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Abstarction.IServices
{
    public interface IFishingSpotServise
    {
        public Task<FishingSpot> CreateFishingSpotAsync(FishingSpot FishingSpot);
        public Task<FishingSpot> UpdateFishingSpot(int FishingSpotd, FishingSpot updatedFishingSpot);
        public Task<FishingSpot> DeleteFishingSpot(int FishingSpotId);
    }
}
