using FM.Domain.Exceptions;
using FM.Domain.Interfaces;
using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Context;
using FM.Infrastructure.Exceptions;
using FM.Infrastructure.Mappers;
using FM.Infrastructure.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FM.Infrastructure.EFRepositories
{
    public class EFVoertuigRepository : IVoertuigRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EFVoertuigRepository> _logger;
        private readonly FleetManagementDbContext _context;
        private readonly IBrandstofTypeRepository _brandstofRepo;
        private readonly ITypeWagenRepository _typeWagenRepo;

        public EFVoertuigRepository(IConfiguration configuration, ILogger<EFVoertuigRepository> logger, FleetManagementDbContext context, IBrandstofTypeRepository brandstofTypeRepository, ITypeWagenRepository typeWagenRepo)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _brandstofRepo = brandstofTypeRepository;
            _typeWagenRepo = typeWagenRepo;
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!await Exists(new DVoertuig() { VoertuigId = id })) throw new EntityDoesNotExistException($"Voertuig with id {id} does not exist");
            try
            {
                var entityToDelete = await _context.Voertuigen.FindAsync(id);
                _context.Voertuigen.Remove(entityToDelete!);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("VoertuigRepo: " + ex.Message, ex);
                if (ex is OperationCanceledException || ex is DbUpdateException 
                    || ex is DbUpdateConcurrencyException) throw;
                throw new VoertuigRepoException("VoertuigRepo: " + ex.Message, ex);
            }
        }

        public async Task<bool> Exists(DVoertuig entity)
        {
            var voertuig = new Voertuig();
            if (entity.VoertuigId == 0)
            {
                voertuig = await _context.Voertuigen.Where(v => v.Chassisnummer == entity.Chassisnummer).FirstOrDefaultAsync();
            }
            else
            {
                voertuig = await _context.Voertuigen.FindAsync(entity.VoertuigId);
            }
            if (voertuig == null) return false;
            return true;
        }

        public async Task<List<DVoertuig>> GetAllAsync()
        {
            try
            {
                var efVoertuigen = await _context.Voertuigen.Where(x => x.IsDeleted == false)
                    .Include(v => v.TypeWagen)
                                                            .Include(v => v.BrandstofType)
                                                            .ToListAsync();
                return efVoertuigen.Select(v => VoertuigMapper.MapToDVoertuig(v)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("VoertuigRepo: " + ex.Message, ex);
                if (ex is VoertuigException || ex is OperationCanceledException
                    || ex is DbUpdateException || ex is DbUpdateConcurrencyException) throw;
                throw new VoertuigRepoException("VoertuigRepo: " + ex.Message, ex);
            }
        }

        public async Task<DVoertuig> GetByIdAsync(int id)
        {
            try
            {
                var efVoertuig = await _context.Voertuigen.Where(x => x.IsDeleted == false)
                    .Include(v => v.TypeWagen)
                                                          .Include(v => v.BrandstofType)
                                                          .Where(v => v.VoertuigId == id)
                                                          .FirstOrDefaultAsync()
                                                          ?? throw new EntityDoesNotExistException($"Voertuig with {id} does not exist");

                return VoertuigMapper.MapToDVoertuig(efVoertuig);
            }
            catch (Exception ex)
            {
                _logger.LogError("VoertuigRepo: " + ex.Message, ex);
                if (ex is VoertuigException || ex is OperationCanceledException
                    || ex is DbUpdateException || ex is DbUpdateConcurrencyException
                    || ex is EntityDoesNotExistException) throw;
                throw new VoertuigRepoException("VoertuigRepo: " + ex.Message, ex);
            }
        }

        public async Task<DVoertuig> InsertAsync(DVoertuig nieuwVoertuig)
        {
            if (await Exists(nieuwVoertuig))
                throw new EntityAlreadyExistsException($"Voertuig with Chassisnummer = {nieuwVoertuig.Chassisnummer} already exists");
            try
            {
                var efBrandstof = await _context.BrandstofTypes.Where(b => b.Type == nieuwVoertuig.BrandstofType)
                                                                .FirstOrDefaultAsync()
                                                                ?? throw new EntityDoesNotExistException($"BrandstofType '{nieuwVoertuig.BrandstofType}' does not exist");
                var efTypeWagen = await _context.TypeWagens.Where(w => w.Type == nieuwVoertuig.TypeWagen)
                                                            .FirstOrDefaultAsync()
                                                            ?? throw new EntityDoesNotExistException($"TypeWagen '{nieuwVoertuig.TypeWagen}' does not exist");
                var newEfEntty = VoertuigMapper.MapToEfVoertuig(nieuwVoertuig, efBrandstof, efTypeWagen);
                var efInsert = await _context.Voertuigen.AddAsync(newEfEntty);
                await _context.SaveChangesAsync();

                nieuwVoertuig.VoertuigId = efInsert.Entity.VoertuigId;
                return nieuwVoertuig;
            }
            catch (Exception ex)
            {
                _logger.LogError("VoertuigRepo: " + ex.Message, ex);
                if (ex is VoertuigException || ex is OperationCanceledException
                    || ex is DbUpdateException || ex is DbUpdateConcurrencyException) throw;
                throw new VoertuigRepoException("VoertuigRepo: " + ex.Message, ex);
            }
        }

        public async Task UpdateAsync(DVoertuig updatetInformation)
        {
            try
            {
                //Getting the original entity from the database
                var originalEntity = await _context.Voertuigen.Where(x => x.IsDeleted == false)
                                                              .Include(v => v.BrandstofType)
                                                              .Include(v => v.TypeWagen)
                                                              .Where(v => v.VoertuigId == updatetInformation.VoertuigId)
                                                              .FirstOrDefaultAsync()
                                                                ?? throw new EntityDoesNotExistException($"Voertuig with id {updatetInformation.VoertuigId} does not exist");
                //Check which properties need to be changed
                // 1 MerkModel
                if (originalEntity.MerkEnModel != updatetInformation.MerkEnModel)
                    originalEntity.MerkEnModel = updatetInformation.MerkEnModel!;
                // 2 Chassisnummer
                if (originalEntity.Chassisnummer != updatetInformation.Chassisnummer)
                    originalEntity.Chassisnummer = updatetInformation.Chassisnummer!;
                // 3 Nummerplaat
                if (originalEntity.Nummerplaat != updatetInformation.Nummerplaat)
                    originalEntity.Nummerplaat = updatetInformation.Nummerplaat!;
                // 4 Brandstof
                if (originalEntity.BrandstofType.Type != updatetInformation.BrandstofType)
                {
                    var updatetBrandstof = _brandstofRepo.Brandstoffen.Where(b => b.Type == updatetInformation.BrandstofType).FirstOrDefault();
                    originalEntity.BrandstofTypeId = updatetBrandstof!.BrandstofTypeId;
                }
                // 5 TypeWagen
                if (originalEntity.TypeWagen.Type != updatetInformation.TypeWagen)
                {
                    var updatetTypeWagen = _typeWagenRepo.Types.Where(tw => tw.Type == updatetInformation.TypeWagen).FirstOrDefault();
                    originalEntity.TypeWagenId = updatetTypeWagen!.TypeWagenId;
                }
                // 6 Kleur
                if (originalEntity.Kleur != updatetInformation.Kleur)
                    originalEntity.Kleur = updatetInformation.Kleur!;
                // 7 AantalDeuren
                if (originalEntity.AantalDeuren != updatetInformation.AantalDeuren)
                    originalEntity.AantalDeuren = updatetInformation.AantalDeuren;

                _context.Voertuigen.Update(originalEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("VoertuigRepo: " + ex.Message, ex);
                if (ex is VoertuigException || ex is OperationCanceledException
                    || ex is DbUpdateException || ex is DbUpdateConcurrencyException
                    || ex is EntityDoesNotExistException) throw;
                throw new VoertuigRepoException("VoertuigRepo: " + ex.Message, ex);
            }
        }
    }
}
