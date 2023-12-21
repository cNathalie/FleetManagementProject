using FM.Domain.Models;

namespace FM.Domain.Interfaces
{
    public interface ITypeWagenRepository
    {
        public List<DTypeWagen> Types { get; }
        public Task<List<DTypeWagen>> GetAllAsync();
        public Task<DTypeWagen> GetByIdAsync(int id);
    }
}
