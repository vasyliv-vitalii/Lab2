using DomainLayer.Models;

namespace DomainLayer.Abstraction.IServices;

public interface IUserService
{
    public int GetCurrentUserId();
    public string GetCurrentUserRole();
    public Task<User> CreateUserAsync(User user);
    public Task<User> UpdateUser( User updatedUser);
    public Task<User> DeleteUser();
}