

namespace FM_Domain.Interfaces
{
    public interface IFMBestuurderRepository
    {
        public List<Bestuurder> Bestuurders { get; }
        public List<Bestuurder> RefreshBestuurders();
        public Bestuurder Insert(Bestuurder bestuurder);
        public void Update(Bestuurder bestuurder);
        public void Delete(Bestuurder bestuurder);
        public bool Exists(Bestuurder bestuurder);
    }
}
