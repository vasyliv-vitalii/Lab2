using DALayer.DataBase;
using DomainLayer.Abstraction.IQueryRepositories;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DALayer.QueryRepositories
{
    public class FishingSpotQueryRepository: IFishingSpotQueryRepository
    { 
        private readonly AppDbContext _context;

        public FishingSpotQueryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FishingSpot>> GetAllFishingSpots()
        {
            return await _context.FishingSpots.ToListAsync();
        }

        public async Task<FishingSpot?> GetFishingSpotById(int FishingSpotId)
        {
            return await _context.FishingSpots.FirstOrDefaultAsync(x => x.Id == FishingSpotId);
        }
    }
}
