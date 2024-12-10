using BLLayer.Authentication.Interfaces;
using DomainLayer.Abstraction.IQueryRepositories;
using DomainLayer.Abstraction.IServices;
using DomainLayer.Models;

namespace BLLayer.Services;

public class UserService : IUserService
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IIdentityInfoGetter _identityInfoGetter;

    public UserService(IUserQueryRepository userQueryRepository, IIdentityInfoGetter identityInfoGetter)
    {
        _userQueryRepository = userQueryRepository;
        _identityInfoGetter = identityInfoGetter;
    }

    public int GetCurrentUserId()
    {
        return _identityInfoGetter.UserId;
    }

    public string GetCurrentUserRole()
    {
        return _identityInfoGetter.UserRole;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.SubscribedFishingSpots = new List<FishingSpot>();
        user.SubscribedRoutes = new List<BikeRoute>();
        return user;
    }

    public async Task<User> UpdateUser(User updatedUser)
    {
        var userId = GetCurrentUserId();
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

    public async Task<User> DeleteUser()
    {
        var userId = GetCurrentUserId();
        var user = await _userQueryRepository.GetUserById(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        return user;
    }
}