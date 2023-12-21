using FM.Domain.Interfaces;
using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Context;
using FM.Infrastructure.Exceptions;
using FM.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace FM.Infrastructure.EFRepositories
{
    public class EFBrandstofTypeRepository : IBrandstofTypeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EFBrandstofTypeRepository> _logger;
        private readonly FleetManagementDbContext _context;
        private List<DBrandstofType> _brandstoffen = new();
        public List<DBrandstofType> Brandstoffen
        {
            get
            {
                if(_brandstoffen != null) return _brandstoffen;
                return GetBrandstoffen()!;
            }
        } // Stored in memory for quick acces

        public EFBrandstofTypeRepository(IConfiguration configuration, ILogger<EFBrandstofTypeRepository> logger, FleetManagementDbContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            GetBrandstoffen();
        }

        public async Task<List<DBrandstofType>> GetAllAsync()
        {
            try
            {
                var efBrandstoffen = await _context.BrandstofTypes.Where(x => x.IsDeleted == false).ToListAsync();
                var dBrandstoffen = efBrandstoffen.Select(b => BrandstofMapper.MapToDBrandstof(b)).ToList();
                return dBrandstoffen;
            }
            catch (Exception ex)
            {
                _logger.LogError("BrandstofTypeRepo: GetAllAsync: " + ex.Message, ex);
                throw new BrandstofTypeRepoException("BrandstofTypeRepo: GetAllAsync: " + ex.Message, ex);
            }
        }

        public async Task<DBrandstofType> GetByIdAsync(int id)
        {
            try
            {
                var efBrandstof = await _context.BrandstofTypes.FindAsync(id) ?? throw new EntityDoesNotExistException($"Brandstoftype with id {id} does not exist");
                var dBrandstof = BrandstofMapper.MapToDBrandstof(efBrandstof);
                return dBrandstof;
            }
            catch (Exception ex)
            {
                if (ex is EntityDoesNotExistException) throw;
                _logger.LogError("BrandstofTypeRepo: GetByIdAsync: " + ex.Message, ex);
                throw new BrandstofTypeRepoException("BrandstofRepo: GetByIdAsync: " + ex.Message, ex);
            }
        }

        private List<DBrandstofType>? GetBrandstoffen()
        {
            _brandstoffen.Clear();
            var efBrandstoffen = _context.BrandstofTypes.ToList();
            _brandstoffen = efBrandstoffen.Select(b => BrandstofMapper.MapToDBrandstof(b)).ToList();
            return _brandstoffen;
        }
    }
}
