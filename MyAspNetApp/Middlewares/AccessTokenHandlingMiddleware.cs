using System.Security.Claims;
using BLLayer.Authentication.Interfaces;

namespace MyAspNetApp.Middlewares;

public class AccessTokenHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public AccessTokenHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IIdentityInfoSetter identityInfoSetter)
    {
        var userIdentityClaim = context.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        var userRoleClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (int.TryParse(userIdentityClaim, out var userId))
        {
            identityInfoSetter.UserId = userId;
        }

        if (!string.IsNullOrEmpty(userRoleClaim))
        {
            Console.WriteLine("dasdsadasdasdsad;");
            identityInfoSetter.UserRole = userRoleClaim;
        }
        
        await _next.Invoke(context);
        
    }
}