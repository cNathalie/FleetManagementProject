using FM.Domain.Models;

namespace FM.Domain.Interfaces
{
    public interface IBestuurderRepository
    {
        public Task<List<DBestuurder>> GetAllAsync();
        public Task<DBestuurder> GetByIdAsync(int id);
        public Task<DBestuurder> InsertAsync(DBestuurder entity);
        public Task UpdateAsync(DBestuurder entity);
        public Task DeleteByIdAsync(int id);
        public Task<bool> Exists(DBestuurder entity);
    }
}
