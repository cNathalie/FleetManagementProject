using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Models;

namespace FM.Infrastructure.Mappers
{
    public class UserRefreshTokenMapper
    {
        public static UserRefreshToken MapToEf(DUserRefreshToken userRefreshToken)
        {
            return new UserRefreshToken
            {
                RefreshTokenId = userRefreshToken.RefreshTokenId,
                UserId = userRefreshToken.UserId,
                RefreshToken = userRefreshToken.RefreshToken!,
                IsActive = userRefreshToken.IsActive
            };
        }

        public static DUserRefreshToken MapToDomain(UserRefreshToken userRefreshToken)
        {
            return new DUserRefreshToken
            {
                RefreshTokenId = userRefreshToken.RefreshTokenId,
                UserId = userRefreshToken.UserId,
                RefreshToken = userRefreshToken.RefreshToken,
                IsActive = (bool)userRefreshToken.IsActive!
            };
        }
    }
}
