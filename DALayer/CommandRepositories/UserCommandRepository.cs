using DomainLayer.Abstarction.ICommandRepositories;
using DomainLayer.Models;

namespace DALayer.CommandRepositories;

public class UserCommandRepository : IUserCommandRepository
{
    public Task<User> CreateUser(User user)
    {
        throw new NotImplementedException();
    }
}