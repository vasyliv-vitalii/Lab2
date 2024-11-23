using DomainLayer.Abstarction.IQueryRepositories;
using DomainLayer.Abstarction.IServices;
using DomainLayer.Models;

namespace BLLayer.Services;

public class UserService : IUserService
{
    private readonly IUserQueryRepository _userQueryRepository;

    public UserService(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        user.SubscribedFishingSpots = new List<FishingSpot>();
        user.SubscribedRoutes = new List<BikeRoute>();
        return user;
    }

    public async Task<User> UpdateUser(int userId, User updatedUser)
    {
        var user = await _userQueryRepository.GetUserById(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        user.Role = updatedUser.Role;
        user.Email = updatedUser.Email;
        user.Username = updatedUser.Username;
        return user;
    }

    public async Task<User> DeleteUser(int userId)
    {
        var user = await _userQueryRepository.GetUserById(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        return user;
    }
}