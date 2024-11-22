using DomainLayer.Models;

namespace DomainLayer.Abstarction.ICommandRepositories;

public interface IUserCommandRepository
{
    public Task<User> CreateUser(User user);
}