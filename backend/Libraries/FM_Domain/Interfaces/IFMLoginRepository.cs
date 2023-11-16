using FM_Domain;

namespace FM_Domain.Interfaces
{
    public interface IFMLoginRepository
    {
        public List<Login> Logins { get; }
        public List<Login> RefreshLogins();
        public void Insert(Login login);
        public void Update(Login login);
        public void Delete(Login login);
        public bool Exists(Login login);
    }
}
