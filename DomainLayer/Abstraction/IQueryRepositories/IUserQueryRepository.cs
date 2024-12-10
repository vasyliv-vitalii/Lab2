using DomainLayer.Models;

namespace DomainLayer.Abstraction.IQueryRepositories;

public interface IUserQueryRepository
{
    public Task<List<User>> GetAllUsers();
    public Task<User?> GetUserById(int userId);
    public Task<User?> GetUserByEmail(string email); 
    public Task<List<FishingSpot>> GetUserFishingSpots(int userId);
}