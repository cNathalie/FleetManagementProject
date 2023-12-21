using FM.Domain.Interfaces;
using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Context;
using FM.Infrastructure.Exceptions;
using FM.Infrastructure.Mappers;
using FM.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FM.Infrastructure.EFRepositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EFUserRepository> _logger;
        private readonly FleetManagementDbContext _context;
        private readonly IEncryptedUserService _userService;

        public EFUserRepository(IConfiguration configuration, ILogger<EFUserRepository> logger, FleetManagementDbContext context, IEncryptedUserService userService)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _userService = userService;
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users.Where(u => u.IsDeleted == false && u.UserId == id).FirstOrDefaultAsync()
                    ?? throw new EntityDoesNotExistException($"User with id {id} does not exist");
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("UserRepo: DeleteById: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException || ex is OperationCanceledException 
                    || ex is DbUpdateException || ex is DbUpdateConcurrencyException) throw;
                throw new UserRepoException("UserRepo: " + ex.Message, ex);
            }
        }

        public bool Exists(DUser entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DUser>> GetAllAsync()
        {
            try
            {
                var users = await _context.Users.Where(u => u.IsDeleted == false).ToListAsync();
                return users.Select(u => UserMapper.MapToDUser(u)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("UserRepo: GetAll: " + ex.Message, ex);
                throw new UserRepoException("UserRepo: " + ex.Message, ex);
            }
        }

        public async Task<DUser> GetByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users.Where(u => u.IsDeleted == false && u.UserId == id).FirstOrDefaultAsync()
                    ?? throw new EntityDoesNotExistException($"User with id {id} does not exist");
                return UserMapper.MapToDUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("UserRepo: GetAll: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException) throw;
                throw new UserRepoException("UserRepo: " + ex.Message, ex);
            }
        }

        public  Task<DUser> InsertAsync(DUser entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(DUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
