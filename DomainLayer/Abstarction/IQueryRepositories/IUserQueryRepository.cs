using DomainLayer.Models;

namespace DomainLayer.Abstarction.IQueryRepositories;

public interface IUserQueryRepository
{
    public Task<List<User>> GetAllUsers();
    public Task<User> GetUserById(int userId);
}