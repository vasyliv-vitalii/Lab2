using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Abstarction.ICommandRepositories
{
    public interface IFishingSpotCommandRepository 
    {
        public Task<FishingSpot> CreateFishingSpot(FishingSpot fishingSpot);
        public Task<FishingSpot> UpdateFishingSpot(FishingSpot fishingSpot);
        public Task DeleteFishingSpot(FishingSpot fishingSpot);
    }
}
