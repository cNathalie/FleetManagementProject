using EF_Infrastructure.Context;
using FM_Domain;
using FM_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EF_Repositories;

public class EFVoertuigRepository : IFMVoertuigRepository
{
    private readonly FleetManagementDbContext _dbContext;
    private List<Voertuig> _voertuigen = new();
    public List<Voertuig> Voertuigen
    {
        get
        {
            if (_voertuigen != null) return _voertuigen;
            return RefreshVoertuigen();
        }
    }

    public EFVoertuigRepository(FleetManagementDbContext dbContext)
    {
        _dbContext = dbContext;
        RefreshVoertuigen();
    }

    public void Delete(Voertuig voertuig)
    {
        if(!Exists(voertuig)) return;
        try
        {
            var efVoertuig = GetEFEntity(voertuig);
            var efRemove = _dbContext.Voertuigen.Remove(efVoertuig).Entity;
            var count = _dbContext.SaveChanges();
            RefreshVoertuigen();
        }
        catch(Exception ex)
        {
            //TODO logging
            throw;
        }
    }

    public bool Exists(Voertuig voertuig)
    {
        return GetEFEntity(voertuig) != null;
    }

    public void Insert(Voertuig voertuig)
    {
        if (Exists(voertuig)) return;

        var efBrandstof = _dbContext.BrandstofTypes.Where(b => b.Type == voertuig.Brandstoftype).FirstOrDefault();
        var efWagen = _dbContext.TypeWagens.Where(w => w.Type == voertuig.Typewagen).FirstOrDefault();

        try
        {
            EF_Infrastructure.Models.Voertuig nieuwVoertuig = new()
            {
                MerkEnModel = voertuig.MerkEnModel,
                Chassisnummer = voertuig.Chassisnummer,
                Nummerplaat = voertuig.Nummerplaat,
                BrandstofTypeId = efBrandstof.BrandstofTypeId,
                TypeWagenId = efWagen.TypeWagenId,
                Kleur = voertuig.Kleur,
                AantalDeuren = voertuig.AantalDeuren
            };
            var efInsert = _dbContext.Voertuigen.Add(nieuwVoertuig).Entity;
            var count = _dbContext.SaveChanges();
            RefreshVoertuigen();
        }
        catch (Exception ex)
        {
            //TODO logging
            throw;
        }
    }

    public List<Voertuig> RefreshVoertuigen()
    {
        _voertuigen.Clear();
        var dbVoertuigen = _dbContext.Voertuigen.Include(v => v.BrandstofType)
                                                .Include(v => v.TypeWagen)
                                                .ToList();
        foreach(var v in dbVoertuigen)
        {
            var voertuig = new Voertuig()
            {
                VoertuigId = v.VoertuigId,
                MerkEnModel = v.MerkEnModel,
                Chassisnummer = v.Chassisnummer,
                Nummerplaat = v.Nummerplaat,
                Brandstoftype = v.BrandstofType.Type,
                Typewagen = v.TypeWagen.Type,
                Kleur = v.Kleur,
                AantalDeuren = v.AantalDeuren
            };
            _voertuigen.Add(voertuig);
        }
        return _voertuigen;
    }

    public void Update(Voertuig voertuig)
    {
        if (!Exists(voertuig)) return;
        var efBrandstof = _dbContext.BrandstofTypes.Where(b => b.Type == voertuig.Brandstoftype).FirstOrDefault();
        var efWagen = _dbContext.TypeWagens.Where(w => w.Type == voertuig.Typewagen).FirstOrDefault();
        try
        {
            var efVoertuig = GetEFEntity(voertuig);
            efVoertuig.MerkEnModel = voertuig.MerkEnModel;
            efVoertuig.Chassisnummer = voertuig.Chassisnummer;
            efVoertuig.Nummerplaat = voertuig.Nummerplaat;
            efVoertuig.BrandstofTypeId = efBrandstof.BrandstofTypeId;
            efVoertuig.TypeWagenId = efWagen.TypeWagenId;
            var efUpdate = _dbContext.Voertuigen.Update(efVoertuig).Entity;
            var count = _dbContext.SaveChanges();
            RefreshVoertuigen();
        }
        catch (Exception ex)
        {
            //TODO logging
            throw;
        }
    }

    private EF_Infrastructure.Models.Voertuig GetEFEntity(Voertuig voertuig)
    {
        if (voertuig.VoertuigId != 0)
        {
            return _dbContext.Voertuigen.Where(v => v.VoertuigId == voertuig.VoertuigId).FirstOrDefault();
        }
        return _dbContext.Voertuigen.Where(v => v.Chassisnummer == voertuig.Chassisnummer).FirstOrDefault();
    }
}
