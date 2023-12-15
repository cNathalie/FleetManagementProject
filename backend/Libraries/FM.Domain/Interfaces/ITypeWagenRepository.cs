using FM.Domain.Models;

namespace FM.Domain.Interfaces
{
    public interface ITypeWagenRepository
    {
        public List<DTypeWagen> Types { get; }
        public Task<List<DTypeWagen>> GetAllAsync();
        public Task<DTypeWagen> GetByIdAsync(int id);
        public Task<DTypeWagen> InsertAsync(DTypeWagen entity);
        public Task UpdateAsync(DTypeWagen entity);
        public Task DeleteByIdAsync(int id);
        public bool Exists(DTypeWagen entity);
    }
}
