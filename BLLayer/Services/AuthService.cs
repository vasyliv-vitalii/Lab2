using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DomainLayer.Abstraction.IQueryRepositories;
using DomainLayer.Abstraction.IServices;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLLayer.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserQueryRepository _userQueryRepository;
    
    public AuthService(IConfiguration configuration, IUserQueryRepository userQueryRepository)
    {
        _configuration = configuration;
        _userQueryRepository = userQueryRepository;
    }
    
    public async Task<(User user, string accessToken)> Authenticate(string email, string password)
    {
        var userEntity = await _userQueryRepository.GetUserByEmail(email);
        
        if (userEntity == null)
        {
            throw new Exception("User with the specified email does not exist");
        }
        
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, userEntity.Password);
    
        if (!isPasswordValid)
        {
            throw new Exception("Invalid password");
        }

        var token = GenerateAccessToken(userEntity);
        return (userEntity, token); 
    }

    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new ("id", user.Id.ToString()),
            new (ClaimTypes.Role, user.Role)
        };

        var jwtSettings = _configuration.GetSection("JwtSettings");
        var jwtKey = jwtSettings["SecretKey"];
        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException("JWT Key is not found in appsettings.json.");
        }
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}