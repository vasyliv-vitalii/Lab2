using DALayer.DataBase;
using DomainLayer.Abstarction.IQueryRepositories;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DALayer.QueryRepositories;

public class UserQueryRepository : IUserQueryRepository
{
    private readonly AppDbContext _context;

    public UserQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(int userId)
    {
        return await _context.Users.FirstOrDefaultAsync(x=> x.Id == userId);
    }

    public async Task<List<FishingSpot>> GetUserFishingSpots(int userId)
    {
        var user = _context.Users.Include(u => u.SubscribedFishingSpots).FirstOrDefault(x => x.Id == userId);
        if (user == null)
        {
            throw new NullReferenceException("User not found");
        }
        return user.SubscribedFishingSpots;
    }
}