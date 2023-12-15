using FM.Domain.Models;

namespace FM.Domain.Interfaces
{
    public interface ITankkaartRepository
    {
        public Task<List<DTankkaart>> GetAllAsync();
        public Task<DTankkaart> GetByIdAsync(int id);
        public Task<DTankkaart> InsertAsync(DTankkaart entity);
        public Task UpdateAsync(DTankkaart entity);
        public Task DeleteByIdAsync(int id);
        public bool Exists(DTankkaart entity);
    }
}
