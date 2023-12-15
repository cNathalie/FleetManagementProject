using FM.Domain.Models;

namespace FM.Domain.Interfaces
{
    public interface IBrandstofTypeRepository
    {
        public List<DBrandstofType> Brandstoffen { get; }
        public Task<List<DBrandstofType>> GetAllAsync();
        public Task<DBrandstofType> GetByIdAsync(int id);
    }
}
