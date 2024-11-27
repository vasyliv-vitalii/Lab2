using DomainLayer.Abstarction.IQueryRepositories;
using DomainLayer.Abstarction.IServices;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Services
{
    public class FishingSpotService : IFishingSpotServise
    {
        private readonly IFishingSpotQueryRepository _fishingRepository;

        public FishingSpotService(IFishingSpotQueryRepository fishingRepository)
        {
            _fishingRepository = fishingRepository;
        }

        public async Task<FishingSpot> CreateFishingSpotAsync(FishingSpot FishingSpot)
        {
            FishingSpot.FishTypes = new List<string>();
            return FishingSpot;
        }

        public async Task<FishingSpot> UpdateFishingSpot(int FishingSpotd, FishingSpot updatedFishingSpot)
        {
            var fishingSpot = await _fishingRepository.GetFishingSpotById(FishingSpotd);
            if (fishingSpot == null)
            {
                throw new Exception("fishingSpot not found");
            }
            fishingSpot.Name = updatedFishingSpot.Name;
            fishingSpot.Coordinates = updatedFishingSpot.Coordinates;
            fishingSpot.FishTypes = updatedFishingSpot.FishTypes;
            fishingSpot.Rating = updatedFishingSpot.Rating;
            return fishingSpot;
        }

        public async Task<FishingSpot> DeleteFishingSpot(int FishingSpotId)
        {
            var fishngSpot = await _fishingRepository.GetFishingSpotById(FishingSpotId);
            if (fishngSpot == null)
            {
                throw new Exception("FishngSpot not found");
            }

            return fishngSpot;
        }
    }
}
