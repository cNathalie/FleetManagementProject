using FM_Domain;
using FM_Domain.Enums;

namespace FM_Domain.Interfaces
{
    public interface IFMLoginRepository
    {
        public List<Login> Logins { get; }
        public List<Login> RefreshLogins();
        public Login Insert(Login login);
        public void Update(Login login);
        public void Delete(Login login);
        public bool Exists(Login login);
        public FMRole Authenticate(Login login);
    }
}
