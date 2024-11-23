using DomainLayer.Models;

namespace DomainLayer.Abstarction.IServices;

public interface IUserService
{
    public Task<User> CreateUserAsync(User user);
    public Task<User> UpdateUser(int userId, User updatedUser);
    public Task<User> DeleteUser(int userId);
}