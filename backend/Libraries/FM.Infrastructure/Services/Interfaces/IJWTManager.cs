using FM.Infrastructure.Resources;
using System.Security.Claims;

namespace FM.Infrastructure.Services.Interfaces
{
    public interface IJWTManager
    {
        Tokens GenerateToken(UserResource user);
        Tokens GenerateRefreshToken(UserResource user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
