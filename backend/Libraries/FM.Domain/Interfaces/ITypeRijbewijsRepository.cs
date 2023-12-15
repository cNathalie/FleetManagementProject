using FM.Domain.Models;

namespace FM.Domain.Interfaces
{
    public interface ITypeRijbewijsRepository
    {
        public Task<List<DTypeRijbewijs>> GetAllAsync();
        public Task<DTypeRijbewijs> GetByIdAsync(int id);
        public Task<DTypeRijbewijs> InsertAsync(DTypeRijbewijs entity);
        public Task UpdateAsync(DTypeRijbewijs entity);
        public Task DeleteByIdAsync(int id);
        public bool Exists(DTypeRijbewijs entity);
    }
}
