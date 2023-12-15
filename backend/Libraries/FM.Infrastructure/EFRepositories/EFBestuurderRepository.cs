using FM.Domain.Interfaces;
using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Context;
using FM.Infrastructure.EntityFramework.Models;
using FM.Infrastructure.Exceptions;
using FM.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FM.Infrastructure.EFRepositories
{
    public class EFBestuurderRepository : IBestuurderRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EFBestuurderRepository> _logger;
        private readonly FleetManagementDbContext _context;

        public EFBestuurderRepository(IConfiguration configuration, ILogger<EFBestuurderRepository> logger, FleetManagementDbContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                var efBestuurder = await _context.Bestuurders.FindAsync(id) ?? throw new EntityDoesNotExistException("The entity you are trying to delete does not exist");
                var efRemove = _context.Bestuurders.Remove(efBestuurder).Entity;
                await _context.SaveChangesAsync();
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError("DeleteByIdAsync: EF: Operation Cancelled: " + ex.Message, ex);
                throw new EFBestuurderRepoException("DeleteByIdAsync:  EF: Operation Cancelled: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteByIdAsync: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException || ex is OperationCanceledException || ex is DbUpdateException
                    || ex is DbUpdateConcurrencyException) throw; // Concurrency conflicts occur when two or more transactions attempt to modify the same data at the same time
                throw new EFBestuurderRepoException("DeleteByIdAsync: " + ex.Message, ex);
            }
        }

        public async Task<bool> Exists(DBestuurder bestuurder)
        {
            return await GetEfEntityAsync(bestuurder) != null;
        }

        private async Task<Bestuurder> GetEfEntityAsync(DBestuurder bestuurder)
        {
            if (bestuurder == null) throw new EFBestuurderRepoException("GetEfEntity: domain Bestuurder is null");
            if (bestuurder?.BestuurderId != null)
            {
                return await _context.Bestuurders.Where( b => b.BestuurderId == bestuurder.BestuurderId && b.IsDeleted == false).FirstOrDefaultAsync() ?? null!;
            }
            return await _context.Bestuurders.Where(b => b.Rijksregisternummer == bestuurder!.Rijksregisternummer && b.IsDeleted == false).FirstOrDefaultAsync() ?? null!;
        }

        public async Task<List<DBestuurder>> GetAllAsync()
        {
            try
            {
                var efBestuurders = await _context.Bestuurders.Where(x => x.IsDeleted == false).ToListAsync();
                var efRijbewijzen = await _context.TypeRijbewijs.ToListAsync();
                return efBestuurders.Select(b => BestuurderMapper.MapToDBestuurder(b, efRijbewijzen)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllAsync: " + ex.Message, ex);
                throw new EFBestuurderRepoException("GetAllAsync: " + ex.Message, ex);
            }

        }

        public async Task<DBestuurder> GetByIdAsync(int id)
        {
            try
            {
                var efBestuurder = await GetEfEntityAsync(new DBestuurder() { BestuurderId = id}) ?? throw new EntityDoesNotExistException("Bestuurder-id does not exist");
                var efRijbewijs = await _context.TypeRijbewijs.FindAsync(efBestuurder.TyperijbewijsId) ?? throw new EntityDoesNotExistException("Rijbewijs-id does not exist");

                return BestuurderMapper.MapToDBestuurder(efBestuurder, efRijbewijs);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetByIdAsync: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException) throw;
                throw new EFBestuurderRepoException("GetByIdAsync: " + ex.Message, ex);
            }

        }

        public async Task<DBestuurder> InsertAsync(DBestuurder bestuurder)
        {
            if (await Exists(bestuurder)) { throw new EntityAlreadyExistsException($"A Bestuurder with RRN {bestuurder.Rijksregisternummer} already exists"); }
            try
            {
                var efRijbewijs = await _context.TypeRijbewijs.Where(t => t.Type == bestuurder.Rijbewijs).FirstOrDefaultAsync()
                    ?? throw new EntityDoesNotExistException($"Rijbewijs type {bestuurder.Rijbewijs} does not exist");

                var newEntity = BestuurderMapper.MapToEFBestuurder(bestuurder, efRijbewijs);
                var efInsert = await _context.Bestuurders.AddAsync(newEntity);
                await _context.SaveChangesAsync();
                bestuurder.BestuurderId = efInsert.Entity.BestuurderId;
                return bestuurder;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetByIdAsync: " + ex.Message, ex);
                if (ex is EFBestuurderRepoException || ex is OperationCanceledException
                    || ex is DbUpdateConcurrencyException || ex is EntityDoesNotExistException) throw;
                throw new EFBestuurderRepoException("InsertAsync: " + ex.Message, ex);
            }
        }

        public async Task UpdateAsync(DBestuurder bestuurder)
        {
            if (!await Exists(bestuurder)) throw new EntityDoesNotExistException("UpdateAsync: the entity you are trying to update does not exist");
            try
            {
                var efBestuurder = await GetEfEntityAsync(bestuurder);
                var efRijbewijs = await _context.TypeRijbewijs.Where(t => t.Type == bestuurder.Rijbewijs).FirstOrDefaultAsync();
                if (efRijbewijs != null)
                {
                    efBestuurder.Naam = bestuurder.Naam!;
                    efBestuurder.Voornaam = bestuurder.Voornaam!;
                    efBestuurder.Adres = bestuurder.Adres!;
                    efBestuurder.Rijksregisternummer = bestuurder.Rijksregisternummer!;
                    efBestuurder.TyperijbewijsId = efRijbewijs!.TypeRijbewijsId;

                    var efUpdate = _context.Bestuurders.Update(efBestuurder);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new EFBestuurderRepoException("UpdateAsync: 'Rijbewijs' could not be matched to database entity");
                }
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError("UpdateAsync: EF: Operation Cancelled: " + ex.Message, ex);
                throw new EFBestuurderRepoException("UpdateAsync:  EF: Operation Cancelled: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateAsync: " + ex.Message, ex);
                throw new EFBestuurderRepoException("UpdateAsync: " + ex.Message, ex);
            }
        }
    }
}
