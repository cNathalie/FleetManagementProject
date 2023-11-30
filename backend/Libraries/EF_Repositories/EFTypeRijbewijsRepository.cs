using EF_Infrastructure.Context;
using FM_Domain;
using FM_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EF_Repositories;

public class EFTypeRijbewijsRepository : IFMTypeRijbewijsRepository
{
    private readonly FleetManagementDbContext _dbContext;

    private List<TypeRijbewijs> _typesRijbewijs = new();
    public List<TypeRijbewijs> TypesRijbewijs
    {
        get
        {
            if (_typesRijbewijs != null) return _typesRijbewijs;
            return RefreshTypesRijbewijs();
        }
    }

    public EFTypeRijbewijsRepository(FleetManagementDbContext context)
    {
        _dbContext = context;
        RefreshTypesRijbewijs();
    }


//---------------ASYNC----------------------------------------------------------------------------


    public async Task<List<TypeRijbewijs>> GetTypesRijbewijsAsync()
    {
        var dbTypesRijbewijs = await _dbContext.TypeRijbewijs.ToListAsync();
        return dbTypesRijbewijs.Select(tr => MapToDomainTypeRijbewijs(tr)).ToList();
    }

    private TypeRijbewijs MapToDomainTypeRijbewijs(EF_Infrastructure.Models.TypeRijbewijs typeRijbewijs)
    {
        return new TypeRijbewijs()
        {
            TypeRijbewijsId = typeRijbewijs.TypeRijbewijsId,
            Type = typeRijbewijs.Type
        };

    }

    public void Delete(TypeRijbewijs typeRijbewijs)
    {
        if(!Exists(typeRijbewijs)) return;

        try
        {
            var efRijbewijs = GetEFEntity(typeRijbewijs);
            var efRemove = _dbContext.TypeRijbewijs.Remove(efRijbewijs).Entity;
            var count = _dbContext.SaveChanges();
            RefreshTypesRijbewijs();
        }
        catch (Exception ex) 
        {
            //TODO logging
            throw;
        }
    }

    public bool Exists(TypeRijbewijs typeRijbewijs)
    {
        return GetEFEntity(typeRijbewijs) != null;
    }

    public void Insert(TypeRijbewijs typeRijbewijs)
    {
        if (Exists(typeRijbewijs)) return;
        try
        {
            EF_Infrastructure.Models.TypeRijbewijs newType = new()
            {
                Type = typeRijbewijs.Type
            };

            var efInsert = _dbContext.TypeRijbewijs.Add(newType).Entity;
            var count = _dbContext.SaveChanges();
            RefreshTypesRijbewijs();
        }
        catch(Exception ex) 
        { 
            //TODO logging
            throw; 
        }
    }

    public List<TypeRijbewijs> RefreshTypesRijbewijs()
    {
        _typesRijbewijs.Clear();
        var dbTypesRijbewijs = _dbContext.TypeRijbewijs.ToList();
        foreach(var type in dbTypesRijbewijs )
        {
            var typeRijbewijs = new TypeRijbewijs()
            {
                TypeRijbewijsId = type.TypeRijbewijsId,
                Type = type.Type
            };

            _typesRijbewijs.Add(typeRijbewijs);
        }
        return _typesRijbewijs;
    }

    public void Update(TypeRijbewijs typeRijbewijs)
    {
        if(!Exists(typeRijbewijs)) return;
        try
        {
            var efTypeRijbewijs = GetEFEntity(typeRijbewijs);
            efTypeRijbewijs.Type = typeRijbewijs.Type;
            var efUpdate = _dbContext.TypeRijbewijs.Update(efTypeRijbewijs).Entity;
            var count = _dbContext.SaveChanges();
            RefreshTypesRijbewijs();
        }
        catch (Exception ex) 
        {
            //TODO logging
            throw;
        }
    }


    private EF_Infrastructure.Models.TypeRijbewijs GetEFEntity(TypeRijbewijs rijbewijs)
    {
        if (rijbewijs.TypeRijbewijsId != 0)
        {
            return _dbContext.TypeRijbewijs.Where(t => t.TypeRijbewijsId == rijbewijs.TypeRijbewijsId).FirstOrDefault();
        }
        return _dbContext.TypeRijbewijs.Where(t => t.Type == rijbewijs.Type).FirstOrDefault(); 
    }
}
