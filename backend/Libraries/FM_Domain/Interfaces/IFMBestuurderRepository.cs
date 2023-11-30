

namespace FM_Domain.Interfaces
{
    public interface IFMBestuurderRepository
    {
        public Task<List<Bestuurder>> GetBestuurdersAsync();
        public Task<Bestuurder> GetBestuurderByIdAsync(int id);
        public  Task<Bestuurder> InsertAsync(Bestuurder bestuurder);
        public  Task UpdateAsync(Bestuurder bestuurder);
        public Task DeleteAsync(Bestuurder bestuurder);
        public bool Exists(Bestuurder bestuurder);

        public List<Bestuurder> GetBestuurders();
        public Bestuurder GetBestuurderById(int id);
        public Bestuurder Insert(Bestuurder bestuurder);
        public void Update(Bestuurder bestuurder);
        public void Delete(Bestuurder bestuurder);

    }
}
