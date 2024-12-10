using DALayer.DataBase;
using DomainLayer.Abstraction.ICommandRepositories;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALayer.CommandRepositories
{
    public class FishingSpotCommandRepository : IFishingSpotCommandRepository
    {

        private readonly AppDbContext _context;

        public FishingSpotCommandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FishingSpot> CreateFishingSpot(FishingSpot fishingSpot)
        {
            _context.FishingSpots.Add(fishingSpot);
            await _context.SaveChangesAsync();
            return fishingSpot;
        }

        public async Task DeleteFishingSpot(FishingSpot fishingSpot)
        {
            _context.FishingSpots.Remove(fishingSpot);
            await _context.SaveChangesAsync();
        }

        public async Task<FishingSpot> UpdateFishingSpot(FishingSpot fishingSpot)
        {
            _context.FishingSpots.Update(fishingSpot);
            await _context.SaveChangesAsync();
            return fishingSpot;
        }
    }
}
