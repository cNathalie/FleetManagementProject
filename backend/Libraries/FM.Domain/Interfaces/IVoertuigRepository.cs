using FM.Domain.Models;

namespace FM.Domain.Interfaces
{
    public interface IVoertuigRepository
    {
        public Task<List<DVoertuig>> GetAllAsync();
        public Task<DVoertuig> GetByIdAsync(int id);
        public Task<DVoertuig> InsertAsync(DVoertuig entity);
        public Task UpdateAsync(DVoertuig entity);
        public Task DeleteByIdAsync(int id);
        public Task<bool> Exists(DVoertuig entity);
    }
}
