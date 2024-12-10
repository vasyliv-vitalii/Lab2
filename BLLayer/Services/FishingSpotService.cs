using DomainLayer.Abstraction.IQueryRepositories;
using DomainLayer.Abstraction.IServices;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Services
{
    public class FishingSpotService : IFishingSpotService
    {
        private readonly IFishingSpotQueryRepository _fishingRepository;
        private readonly IUserService _userService;

        public FishingSpotService(IFishingSpotQueryRepository fishingRepository, IUserService userService)
        {
            _fishingRepository = fishingRepository;
            _userService = userService;
        }

        public async Task<FishingSpot> CreateFishingSpotAsync(FishingSpot fishingSpot)
        {
            var currentUserRole = _userService.GetCurrentUserRole();
            if (currentUserRole.ToLower() != "admin")
            {
                throw new UnauthorizedAccessException();
            }
            fishingSpot.FishTypes = new List<string>();
            return fishingSpot;
        }

        public async Task<FishingSpot> UpdateFishingSpot(int fishingSpotd, FishingSpot updatedFishingSpot)
        {
            var fishingSpot = await _fishingRepository.GetFishingSpotById(fishingSpotd);
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

        public async Task<FishingSpot> DeleteFishingSpot(int fishingSpotId)
        {
            var fishngSpot = await _fishingRepository.GetFishingSpotById(fishingSpotId);
            if (fishngSpot == null)
            {
                throw new Exception("FishngSpot not found");
            }

            return fishngSpot;
        }
    }
}
