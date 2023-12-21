using FM.Domain.Models;
using FM.Infrastructure.Resources;

namespace FM.Infrastructure.Services.Interfaces
{
    public interface IEncryptedUserService
    {
        Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken);
        Task<AuthenticatedUserResource> Login(LoginResource resource, CancellationToken cancellationToken);
        Task<DUserRefreshToken> AddUserRefreshTokens(DUserRefreshToken user);
        Task<DUserRefreshToken> GetSavedRefreshTokens(int userId, string refreshtoken);
        Task DeleteUserRefreshTokens(int userId, string refreshtoken);
        Task<Tokens> RefreshToken(Tokens tokens);
        Task Logout(Tokens tokens);
        Task<string> Verify(Tokens tokens);
    }
}
