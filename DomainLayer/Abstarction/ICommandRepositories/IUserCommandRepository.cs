using DomainLayer.Models;

namespace DomainLayer.Abstarction.ICommandRepositories;

public interface IUserCommandRepository
{
    public Task<User> CreateUser(User user);
    public Task<User> UpdateUser(User user);
    public Task DeleteUser(User user);
}