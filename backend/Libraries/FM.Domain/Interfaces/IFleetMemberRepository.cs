using FM.Domain.Models;

namespace FM.Domain.Interfaces
{
    public interface IFleetMemberRepository
    {
        public Task<List<DFleetMemberVerbose>> GetAllAsync();
        public Task<DFleetMemberVerbose> GetByIdAsync(int id);
        public Task<DFleetMemberVerbose> InsertAsync(DFleetMemberIds entity);
        public Task UpdateAsync(DFleetMemberIds entity);
        public Task DeleteByIdAsync(int id);
        public bool Exists(DFleetMemberVerbose entity);
    }
}
