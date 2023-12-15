using FM.Domain.Interfaces;
using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Context;
using FM.Infrastructure.Exceptions;
using FM.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FM.Infrastructure.EFRepositories
{
    public class EFTypeWagenRepository : ITypeWagenRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EFTypeWagenRepository> _logger;
        private readonly FleetManagementDbContext _context;
        private List<DTypeWagen> _types = new();
        public List<DTypeWagen> Types
        {
            get
            {
                if(_types != null) return _types;
                return GetTypes()!;
            }
        } // Stored in memory for quick acces in other repos

        public EFTypeWagenRepository(IConfiguration configuration, ILogger<EFTypeWagenRepository> logger, FleetManagementDbContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            GetTypes();
        }

        private List<DTypeWagen>? GetTypes()
        {
            _types.Clear();
            var types = _context.TypeWagens.ToList() ?? throw new EntityDoesNotExistException("No 'TypeWagen' entities in database");
            _types = types.Select(t => TypeWagenMapper.MapToDTypeWagen(t)).ToList();
            return _types;
        }

        public async Task<List<DTypeWagen>> GetAllAsync()
        {
            try
            {
                var types = await _context.TypeWagens.Where(x => x.IsDeleted == false).ToListAsync() ?? throw new EntityDoesNotExistException("No 'TypeWagen' entities in database"); ;
                return types.Select(t => TypeWagenMapper.MapToDTypeWagen(t)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("TypeWagenRepo: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException) throw;
                throw new TypeWagenException("TypeWagenRepo: " + ex.Message, ex);
            }
        }

        public async Task<DTypeWagen> GetByIdAsync(int id)
        {
            try
            {
                var type = await _context.TypeWagens.FindAsync(id) ?? throw new EntityDoesNotExistException($"'TypeWagen' with id {id} doesn not exist"); ;
                return TypeWagenMapper.MapToDTypeWagen(type);
            }
            catch (Exception ex)
            {
                _logger.LogError("TypeWagenRepo: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException) throw;
                throw new TypeWagenException("TypeWagenRepo: " + ex.Message, ex);
            }
        }
    }
}
