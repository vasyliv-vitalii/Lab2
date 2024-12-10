using DomainLayer.Models;

namespace DomainLayer.Abstraction.IServices;

public interface IAuthService
{
    public Task<(User user, string accessToken)> Authenticate(string email, string password);
    public string GenerateAccessToken(User user);
}