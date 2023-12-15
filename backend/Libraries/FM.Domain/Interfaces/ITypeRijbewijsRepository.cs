using FM.Domain.Models;

namespace FM.Domain.Interfaces
{
    public interface ITypeRijbewijsRepository
    {
        public Task<List<DTypeRijbewijs>> GetAllAsync();
        public Task<DTypeRijbewijs> GetByIdAsync(int id);
    }
}
