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
    public class EFTankkaartRepository : ITankkaartRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EFTankkaartRepository> _logger;
        private readonly FleetManagementDbContext _context;

        public EFTankkaartRepository(IConfiguration configuration, ILogger<EFTankkaartRepository> logger, FleetManagementDbContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        #region CRUD-Operations
        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                var efTankkaart = await _context.Tankkaarten.FindAsync(id) ?? throw new EntityDoesNotExistException($"Tankkaart with id {id} does not exist");
                var efRemove = _context.Tankkaarten.Remove(efTankkaart).Entity;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("TankkaartRepo: DeleteByIdAsync: " + ex.Message, ex);
                if (ex is OperationCanceledException || ex is EntityDoesNotExistException) throw;
                throw new FleetMemberRepoException(ex.Message, ex);
            }
        }

        public async Task<List<DTankkaart>> GetAllAsync()
        {
            try
            {
                var efTankkaarten = await _context.Tankkaarten.Where(x => x.IsDeleted == false)
                    .Include(t => t.BrandstofType)
                                                              .ToListAsync();
                return efTankkaarten.Select(t => TankkaartMappper.MapToDTankkaart(t)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("TankkaartRepo: GetAllAsync: " + ex.Message, ex);
                if (ex is OperationCanceledException) throw;
                throw new TankkaartRepoException(ex.Message, ex);
            }
        }

        public async Task<DTankkaart> GetByIdAsync(int id)
        {
            try
            {
                var efTankkaart = await _context.Tankkaarten.Where(x => x.IsDeleted == false)   
                                                        .Include(t => t.BrandstofType)
                                                        .Where(t => t.TankkaartId == id)
                                                        .FirstOrDefaultAsync()
                                                        ?? throw new EntityDoesNotExistException($"Tankkaart with id {id} does not exist");
                return TankkaartMappper.MapToDTankkaart(efTankkaart);
            }
            catch (Exception ex)
            {
                _logger.LogError("TankkaartRepo: GetByIdAsync: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException || ex is OperationCanceledException) throw;
                throw new TankkaartRepoException(ex.Message, ex);
            }

        }

        public async Task<DTankkaart> InsertAsync(DTankkaart newTankkaart)
        {
            try
            {
                await IsKaartnummerUnique(newTankkaart.Kaartnummer);
                await IsGeldigheidsdatumValid(newTankkaart.Geldigheidsdatum);

                var efBrandstof = await GetEfBrandstofByType(newTankkaart.Brandstoftype);
                var newEfTankkaart = TankkaartMappper.MapToEfTankkaart(newTankkaart, efBrandstof);
               
                var efInsert = await _context.Tankkaarten.AddAsync(newEfTankkaart);
                await _context.SaveChangesAsync();

                return TankkaartMappper.MapToDTankkaart(efInsert.Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("TankkaartRepo: InsertAsync: " + ex.Message, ex);
                if (ex is EntityAlreadyExistsException || ex is EntityDoesNotExistException
                    || ex is OperationCanceledException
                    || ex is DbUpdateException
                    || ex is DateTimeInThePastException) { throw; }
                throw new TankkaartRepoException(ex.Message, ex);
            }
        }

        public async Task UpdateAsync(DTankkaart updatetInformation)
        {
            try
            {
                var updatetBrandstof = await GetEfBrandstofByType(updatetInformation.Brandstoftype);
                var originalEntity = await _context.Tankkaarten.FindAsync(updatetInformation.TankkaartId) 
                    ?? throw new EntityDoesNotExistException($"Tankkaart with id {updatetInformation.TankkaartId} does not exist");
                
                if (originalEntity!.Kaartnummer != updatetInformation.Kaartnummer)
                {
                    await IsKaartnummerUnique(updatetInformation.Kaartnummer);
                    originalEntity.Kaartnummer = updatetInformation.Kaartnummer;
                }
                if (originalEntity!.Geldigheidsdatum != updatetInformation.Geldigheidsdatum)
                {
                    await IsGeldigheidsdatumValid(updatetInformation.Geldigheidsdatum);
                    originalEntity.Geldigheidsdatum = updatetInformation.Geldigheidsdatum;
                }
                if (originalEntity.Pincode != updatetInformation.Pincode)
                {
                    originalEntity.Pincode = updatetInformation.Pincode;
                }
                if (originalEntity!.BrandstofTypeId != updatetBrandstof.BrandstofTypeId)
                {
                    originalEntity.BrandstofTypeId = updatetBrandstof.BrandstofTypeId;
                }
                if (originalEntity.Actief != updatetInformation.IsActief)
                {
                    if(updatetInformation.IsActief == true)
                    {
                        await CanBeActive(updatetInformation.Geldigheidsdatum);
                    }
                    originalEntity.Actief = updatetInformation.IsActief;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TankkaartRepo: UpdateAsync: " + ex.Message, ex);
                if (ex is EntityAlreadyExistsException || ex is EntityDoesNotExistException
                    || ex is EntityNotAvailableException || ex is OperationCanceledException
                    || ex is DbUpdateException || ex is DateTimeInThePastException
                    || ex is ActiveCardException) { throw; }
                throw new TankkaartRepoException(ex.Message, ex);
            }
        }
        #endregion

        #region HelperMethods
        private async Task IsGeldigheidsdatumValid(DateTime geldigheidsdatum)
        {
            if(geldigheidsdatum < DateTime.Now.Date)
            {
                throw new DateTimeInThePastException("Geldigheidsdatum cannot be set in the past");
            }
            await Task.Delay(0);
            return;
        }

        private async Task CanBeActive(DateTime geldigheidsdatum)
        {
            if(geldigheidsdatum.Date < DateTime.Now.Date)
            {
                throw new ActiveCardException("-Tankkaart- with -Geldigheidsdatum- in the past cannot be set to -Actief-");
            };
            await Task.Delay(0);
            return;
        }


        private async Task<BrandstofType> GetEfBrandstofByType(string? type)
        {
            var efBrandstof = await _context.BrandstofTypes.Where(b => b.Type == type).FirstOrDefaultAsync()
                                    ?? throw new EntityDoesNotExistException($"Brandstoftype '{type}' does not exist");
            return efBrandstof;
        }

        private async Task IsKaartnummerUnique(int kaartnummer)
        {
            var available = await _context.Tankkaarten.Where(t => t.Kaartnummer == kaartnummer).FirstOrDefaultAsync();
            if (available != null) throw new EntityAlreadyExistsException($"Tankkaart with kaartnummer: {kaartnummer} already exists");
        }

        public bool Exists(DTankkaart entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
