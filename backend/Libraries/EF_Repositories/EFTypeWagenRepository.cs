using EF_Infrastructure.Context;
using FM_Domain;
using FM_Domain.Interfaces;

namespace EF_Repositories;

public class EFTypeWagenRepository : IFMTypeWagenRepository
{
    private readonly FleetManagementDbContext _dbContext;
    private List<TypeWagen> _typeWagens = new();
    public List<TypeWagen> TypeWagens
    {
        get
        {
            if (_typeWagens != null) return _typeWagens;
            return RefreshTypeWagens();
        }
    }

    public EFTypeWagenRepository(FleetManagementDbContext dbContext)
    {
        _dbContext = dbContext;
        RefreshTypeWagens();
    }

    public void Delete(TypeWagen typeWagen)
    {
        if (!Exists(typeWagen)) return;
        try
        {
            var efTypeWagen = GetEFEntity(typeWagen);
            var efRemove = _dbContext.TypeWagens.Remove(efTypeWagen).Entity;
            var count = _dbContext.SaveChanges();
            RefreshTypeWagens();
        }
        catch (Exception ex)
        {
            //TODO logging
            throw;
        }
    }

    public bool Exists(TypeWagen typeWagen)
    {
        return GetEFEntity(typeWagen) != null;
    }

    public void Insert(TypeWagen typeWagen)
    {
        if (Exists(typeWagen)) return;
        try
        {
            EF_Infrastructure.Models.TypeWagen nieuwtypeWagen = new()
            {
                Type = typeWagen.Type,
            };

            var efInsert = _dbContext.TypeWagens.Add(nieuwtypeWagen);
            var count = _dbContext.SaveChanges();
            RefreshTypeWagens();
        }
        catch (Exception ex)
        {
            //TODO logging
            throw;
        }
    }

    public List<TypeWagen> RefreshTypeWagens()
    {
        _typeWagens.Clear();
        var dbTypeWagens = _dbContext.TypeWagens.ToList();
        foreach (var tw in dbTypeWagens)
        {
            var typeWagen = new TypeWagen()
            {
                TypeWagenId = tw.TypeWagenId,
                Type = tw.Type
            };

            _typeWagens.Add(typeWagen);
        }
        return _typeWagens;
    }

    public void Update(TypeWagen typeWagen)
    {
        if (!Exists(typeWagen)) return;

        try
        {
            var efTypeWagen = GetEFEntity(typeWagen);
            efTypeWagen.Type = typeWagen.Type;
            var efUpdate = _dbContext.TypeWagens.Update(efTypeWagen).Entity;
            var count = _dbContext.SaveChanges();
            RefreshTypeWagens();
        }
        catch(Exception ex)
        {
            //TODO logging
            throw;
        }
    }

    private EF_Infrastructure.Models.TypeWagen GetEFEntity(TypeWagen typeWagen)
    {
        if (typeWagen.TypeWagenId != 0)
        {
            return _dbContext.TypeWagens.Where(tw => tw.TypeWagenId == typeWagen.TypeWagenId).FirstOrDefault();
        }
        return _dbContext.TypeWagens.Where(tw => tw.Type == typeWagen.Type).FirstOrDefault();
    }
}
