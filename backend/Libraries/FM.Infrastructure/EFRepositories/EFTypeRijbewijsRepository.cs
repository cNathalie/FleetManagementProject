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
    public class EFTypeRijbewijsRepository : ITypeRijbewijsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EFTypeRijbewijsRepository> _logger;
        private readonly FleetManagementDbContext _context;

        public EFTypeRijbewijsRepository(IConfiguration configuration, ILogger<EFTypeRijbewijsRepository> logger, FleetManagementDbContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public async Task<List<DTypeRijbewijs>> GetAllAsync()
        {
            try
            {
                var efRijbewijzen = await _context.TypeRijbewijs.Where(x => x.IsDeleted == false).ToListAsync();
                return efRijbewijzen.Select(r => RijbewijsMapper.MapToDTypeRijbewijs(r)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("TypeRijbewijsRepo: GetAllAsync: " + ex.Message, ex);
                if (ex is OperationCanceledException) throw;
                throw new TypeRijbewijsRepoException(ex.Message, ex);
            }
        }

        public async Task<DTypeRijbewijs> GetByIdAsync(int id)
        {
            try
            {
                var efRijbewijs = await _context.TypeRijbewijs.FindAsync(id) 
                    ?? throw new EntityDoesNotExistException($"TypeRijbewijs with id {id} does not exist");
                return RijbewijsMapper.MapToDTypeRijbewijs(efRijbewijs);
            }
            catch (Exception ex)
            {
                _logger.LogError("TypeRijbewijsRepo: GetByIdAsync: " + ex.Message, ex);
                if (ex is OperationCanceledException || ex is EntityDoesNotExistException) throw;
                throw new TypeRijbewijsRepoException(ex.Message, ex);
            }
        }
    }
}
