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
    public class EFFleetMemberRepository : IFleetMemberRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EFFleetMemberRepository> _logger;
        private readonly FleetManagementDbContext _context;

        public EFFleetMemberRepository(IConfiguration configuration, ILogger<EFFleetMemberRepository> logger, FleetManagementDbContext context)
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
                var efFleetMember = await _context.FleetMembers.FindAsync(id) ?? throw new EntityDoesNotExistException($"FleetMember with id {id} does not exist");
                var efRemove = _context.FleetMembers.Remove(efFleetMember).Entity;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("FleetMemberRepo: DeleteByIdAsync: " + ex.Message, ex);
                if (ex is OperationCanceledException || ex is EntityDoesNotExistException) throw;
                throw new FleetMemberRepoException(ex.Message, ex);
            }
        }

        public async Task<List<DFleetMemberVerbose>> GetAllAsync()
        {
            try
            {
                var efFleetmembers = await _context.FleetMembers.Where(x => x.IsDeleted == false)
                                                                .Include(fm => fm.Bestuurder)
                                                                .Include(fm => fm.Tankkaart)
                                                                .Include(fm => fm.Voertuig)
                                                                .ToListAsync();
                return efFleetmembers.Select(fm => FleetMemberMapper.MapToDFleetMember(fm)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("FleetMemberRepo: GetAllAsync: " + ex.Message, ex);
                if (ex is OperationCanceledException) throw;
                throw new FleetMemberRepoException(ex.Message, ex);
            }
        }

        public async Task<DFleetMemberVerbose> GetByIdAsync(int id)
        {
            try
            {
                var efMember = await _context.FleetMembers.Include(fm => fm.Bestuurder)
                                                          .Include(fm => fm.Voertuig)
                                                          .Where(fm => fm.FleetId == id)
                                                          .FirstOrDefaultAsync()
                                                          ?? throw new EntityDoesNotExistException($"FleetMember with id {id} does not exist");
                return FleetMemberMapper.MapToDFleetMember(efMember);
            }
            catch(Exception ex)
            {
                _logger.LogError("FleetMemberRepo: GetByIdAsync: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException || ex is OperationCanceledException) throw;
                throw new FleetMemberRepoException(ex.Message, ex);
            }

        }

        public async Task<DFleetMemberVerbose> InsertAsync(DFleetMemberIds newMember)
        {
            try
            {
                await ValuesAreValid(newMember);
                await ValuesAreAvailable(newMember);

                var newEfMember = FleetMemberMapper.MapToEfFleetMember(newMember);
                var efInsert = await _context.FleetMembers.AddAsync(newEfMember);
                await _context.SaveChangesAsync();

                return FleetMemberMapper.MapToDFleetMemberVerbose(efInsert.Entity.FleetId, efInsert.Entity.Bestuurder, efInsert.Entity.Tankkaart, efInsert.Entity.Voertuig);
            }
            catch (Exception ex)
            {
                _logger.LogError("FleetMemberRepo: InsertAsync: " + ex.Message, ex);
                if (ex is EntityAlreadyExistsException || ex is EntityDoesNotExistException
                    || ex is EntityNotAvailableException || ex is OperationCanceledException 
                    || ex is DbUpdateException) { throw; }
                throw new FleetMemberRepoException(ex.Message, ex);
            }
        }

        public async Task UpdateAsync(DFleetMemberIds updatetInformation)
        {
            try
            {
                await ValuesAreValid(updatetInformation); // 1. do the new values exist in database?

                var originalEntity = await _context.FleetMembers.Where(fm => fm.FleetId == updatetInformation.FleetId).FirstOrDefaultAsync(); // 2. get original entity with this id

                if (originalEntity!.BestuurderId != updatetInformation.BestuurderId) // 3. if the value is changed
                {
                    await IsBestuurderAvailable(updatetInformation.BestuurderId); // 4. check if the new value is available
                    originalEntity.BestuurderId = updatetInformation.BestuurderId; // 5. assign new value to property
                }

                if (originalEntity!.TankkaartId != updatetInformation.TankkaartId) // repeat for every value
                {
                    await IsTankkaartAvailable(updatetInformation.TankkaartId);
                    originalEntity.TankkaartId = updatetInformation.TankkaartId;
                }

                if (originalEntity!.VoertuigId != updatetInformation.VoertuigId)
                {
                    await IsVoertuigAvailable(updatetInformation.VoertuigId);
                    originalEntity.VoertuigId = updatetInformation.VoertuigId;
                }

                var efUpdate = _context.FleetMembers.Update(originalEntity).Entity;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("FleetMemberRepo: UpdateAsync: " + ex.Message, ex);
                if (ex is EntityAlreadyExistsException || ex is EntityDoesNotExistException 
                    || ex is EntityNotAvailableException || ex is OperationCanceledException
                    || ex is DbUpdateException) { throw; }
                throw new FleetMemberRepoException(ex.Message, ex);
            }
        }
        #endregion

        #region HelperMethods
        private async Task ValuesAreValid(DFleetMemberIds member)
        {
            // Check if the values exist in a not-deleted state in the database 
            var efBestuurder = await _context.Bestuurders.Where(x => x.IsDeleted == false && x.BestuurderId == member.BestuurderId).FirstOrDefaultAsync() ?? throw new EntityDoesNotExistException($"Bestuurder with id {member.BestuurderId} does not exist");
            var efTankkaart = await _context.Tankkaarten.Where(x => x.IsDeleted == false &&  x.Actief == true && x.TankkaartId == member.TankkaartId).FirstOrDefaultAsync() ?? throw new EntityDoesNotExistException($"Tankkaart with id {member.TankkaartId} does not exist");
            var efVoertuig = await _context.Voertuigen.Where(x => x.IsDeleted == false && x.VoertuigId == member.VoertuigId).FirstOrDefaultAsync() ?? throw new EntityDoesNotExistException($"Voertuig with id {member.VoertuigId} does not exist");
        }
        private async Task ValuesAreAvailable(DFleetMemberIds member)
        {
            // Is the value being used in a non-deleted entity in the database ?
            await IsBestuurderAvailable(member.BestuurderId) ;
            await IsTankkaartAvailable(member.TankkaartId); 
            await IsVoertuigAvailable(member.VoertuigId) ;
            
            if (await CombinationExists(member)) throw new EntityAlreadyExistsException("This FleetMember already exists");
        }

        private async Task IsVoertuigAvailable(int voertuigId)
        {
            var available = await _context.FleetMembers.Where(fm => fm.VoertuigId == voertuigId && fm.IsDeleted == false).FirstOrDefaultAsync(); 
            if(available != null) throw new EntityNotAvailableException($"Voertuig with id {voertuigId} is not available");
        }
        private async Task IsBestuurderAvailable(int bestuurderId)
        {
            var available = await _context.FleetMembers.Where(fm => fm.BestuurderId == bestuurderId && fm.IsDeleted == false).FirstOrDefaultAsync();
            if (available != null) throw new EntityNotAvailableException($"Bestuurder with id {bestuurderId} is not available");
        }
        private async Task IsTankkaartAvailable(int tankkaartId)
        {
            var available = await _context.FleetMembers.Where(fm => fm.TankkaartId == tankkaartId && fm.IsDeleted == false).FirstOrDefaultAsync();
            if (available != null) throw new EntityNotAvailableException($"Tankkaart with id {tankkaartId} is not available");
        }

        public async Task<bool> CombinationExists(DFleetMemberIds member)
        {
            var fm = await _context.FleetMembers.Where(fm => fm.BestuurderId == member.BestuurderId && fm.TankkaartId == member.TankkaartId && fm.VoertuigId == member.VoertuigId && fm.IsDeleted == false)
                                                .FirstOrDefaultAsync();
            return fm != null;
        }

        public async Task<FleetMember> GetEfEntity(DFleetMemberVerbose member)
        {
            if (member.FleetId != 0)
            {
                return await _context.FleetMembers.Where(fm => fm.FleetId == member.FleetId).FirstOrDefaultAsync() ?? null!;
            }
            return await _context.FleetMembers.Where(fm => fm.TankkaartId == member.TankaartId).FirstOrDefaultAsync() ?? null!;
        }

        public bool Exists(DFleetMemberVerbose member)
        {
            return GetEfEntity(member) != null;
        }


        #endregion

    }
}
