using FM.Domain.Enums;
using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Models;

namespace FM.Infrastructure.Mappers
{
    public class UserMapper
    {
        public static DUser MapToDUser(User user)
        {
            var domainUser = new DUser()
            {
                UserId = user.UserId,
                Email = user.Email,
                PasswordSalt = user.PasswordSalt,
                PasswordHash = user.PasswordHash,
                Role = (FMRole)Enum.Parse(typeof(FMRole), user.Role)
            };
            return domainUser;
        }
    }
}
