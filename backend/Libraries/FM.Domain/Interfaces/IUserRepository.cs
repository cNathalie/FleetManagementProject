using FM.Domain.Models;

namespace FM.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<DUser>> GetAllAsync();
        public Task<DUser> GetByIdAsync(int id);
        public Task<DUser> InsertAsync(DUser entity);
        public Task UpdateAsync(DUser entity);
        public Task DeleteByIdAsync(int id);
        public bool Exists(DUser entity);
    }
}
