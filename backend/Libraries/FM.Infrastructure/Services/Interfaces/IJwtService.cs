using FM.Infrastructure.Resources;

namespace FM.Infrastructure.Services.Interfaces
{
    public interface IJwtService
    {
        public string GreateJwtToken(UserResource user);
    }
}
