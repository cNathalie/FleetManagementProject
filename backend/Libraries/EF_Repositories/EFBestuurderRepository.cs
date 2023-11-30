using EF_Infrastructure.Context;
using FM_Domain;
using FM_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EF_Repositories;

public class EFBestuurderRepository : IFMBestuurderRepository
{
 
    private readonly FleetManagementDbContext _dbContext;
    private readonly IFMTypeRijbewijsRepository _rijbewijsRepo;
    private readonly List<TypeRijbewijs> _rijbewijzen;






    // Methodes
    private Bestuurder MapToDomainBestuurder(EF_Infrastructure.Models.Bestuurder efBestuurder, List<EF_Infrastructure.Models.TypeRijbewijs> typeRijbewijzen)
    {
        var bestuurder = new Bestuurder
        {
            BestuurderId = efBestuurder.BestuurderId,
            Naam = efBestuurder.Naam,
            Voornaam = efBestuurder.Voornaam,
            Adres = efBestuurder.Adres,
            Rijksregisternummer = efBestuurder.Rijksregisternummer,
            Rijbewijs = typeRijbewijzen
                .Where(t => t.TypeRijbewijsId == efBestuurder.TyperijbewijsId)?
                .FirstOrDefault()?.Type ?? ""
        };

        return bestuurder;
    }

    #region ASYNC

    public async Task<List<Bestuurder>> GetBestuurdersAsync()
    {
        var dbBestuurders = await _dbContext.Bestuurders.ToListAsync();
        var dbtypesRijbewijs = await _dbContext.TypeRijbewijs.ToListAsync();

        return dbBestuurders.Select(b => MapToDomainBestuurder(b, dbtypesRijbewijs)).ToList();
    }


    public async Task<Bestuurder> GetBestuurderByIdAsync(int id)
    {
        var dbBestuurder = await _dbContext.Bestuurders.FindAsync(id);
        var dbTypeRijbewijs = await _dbContext.TypeRijbewijs.FindAsync(dbBestuurder.TyperijbewijsId);

        return dbBestuurder != null ? MapToDomainBestuurder(dbBestuurder, new List<EF_Infrastructure.Models.TypeRijbewijs> { dbTypeRijbewijs }) : null;
    }

    public async Task<Bestuurder> InsertAsync(Bestuurder bestuurder)
    {
        if (Exists(bestuurder))
        {
            bestuurder.BestuurderId = (await GetEFBestuurderAsync(bestuurder)).BestuurderId;
            return bestuurder;
        }

        try
        {
            var efTypeRijbewijs = await _dbContext.TypeRijbewijs
                .Where(t => t.Type == bestuurder.Rijbewijs)
                .FirstOrDefaultAsync();

            var nieuweBestuurder = new EF_Infrastructure.Models.Bestuurder
            {
                Naam = bestuurder.Naam,
                Voornaam = bestuurder.Voornaam,
                Adres = bestuurder.Adres,
                Rijksregisternummer = bestuurder.Rijksregisternummer,
                TyperijbewijsId = efTypeRijbewijs.TypeRijbewijsId
            };

            var efBestuurder = _dbContext.Bestuurders.Add(nieuweBestuurder).Entity;
            await _dbContext.SaveChangesAsync();

            bestuurder.BestuurderId = efBestuurder.BestuurderId;

            return bestuurder;
        }
        catch (Exception ex)
        {
            // TODO: Logging
            throw;
        }
    }

    public async Task UpdateAsync(Bestuurder bestuurder)
    {
        if (!Exists(bestuurder))
        {
            return;
        }

        try
        {
            var updateBestuurder = await GetEFBestuurderAsync(bestuurder);
            var efTypeRijbewijs = await _dbContext.TypeRijbewijs
                .Where(t => t.Type == bestuurder.Rijbewijs)
                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(bestuurder.Naam))
            {
                updateBestuurder.Naam = bestuurder.Naam;
            }

            // Update other properties similarly

            _dbContext.Update(updateBestuurder);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("An exception has occurred while updating Bestuurder: ", ex);
            throw;
        }
    }

    public async Task DeleteAsync(Bestuurder bestuurder)
    {
        if (!Exists(bestuurder))
        {
            return;
        }

        try
        {
            var efBestuurder = await GetEFBestuurderAsync(bestuurder);
            _dbContext.Bestuurders.Remove(efBestuurder);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("An exception has occurred while deleting Bestuurder: ", ex);
            throw;
        }
    }

    private Task<EF_Infrastructure.Models.Bestuurder?> GetEFBestuurderAsync(Bestuurder bestuurder)
    {
        return bestuurder.BestuurderId != 0
            ? _dbContext.Bestuurders.Where(b => b.BestuurderId == bestuurder.BestuurderId).FirstOrDefaultAsync()
            : _dbContext.Bestuurders
                .Where(b => b.Rijksregisternummer == bestuurder.Rijksregisternummer)
                .FirstOrDefaultAsync();
    }

    #endregion

    public bool Exists(Bestuurder bestuurder)
    {
        return GetEFBestuurderAsync(bestuurder).Result != null;
    }


    #region SYNC

    public List<Bestuurder> GetBestuurders()
    {
        var dbBestuurders = _dbContext.Bestuurders.ToList();
        var dbTypesRijbewijs = _dbContext.TypeRijbewijs.ToList();
        var domainBestuurders = dbBestuurders.Select( b => MapToDomainBestuurder(b, dbTypesRijbewijs)).ToList();
        return domainBestuurders;
    }

    public Bestuurder GetBestuurderById(int id)
    {
        var dbBestuurder = _dbContext.Bestuurders.Where(b => b.BestuurderId ==  id).FirstOrDefault();
        
    }

    public Bestuurder Insert(Bestuurder bestuurder) // een nieuwe bestuurder toevoegen aan de database
    {
        if (Exists(bestuurder))
        {
            bestuurder.BestuurderId = GetEFBestuurder(bestuurder).BestuurderId;
            return bestuurder;
        }
        try
        {
            var efTypeRijbewijs = _dbContext.TypeRijbewijs.Where(t => t.Type == bestuurder.Rijbewijs).FirstOrDefault();
            //Stap 1: Omzetten van het interne domein-model naar het EntityFramework-model
            EF_Infrastructure.Models.Bestuurder nieuweBestuurder = new()
            {
                BestuurderId = bestuurder.BestuurderId,
                Naam = bestuurder.Naam,
                Voornaam = bestuurder.Voornaam,
                Adres = bestuurder.Adres,
                Rijksregisternummer = bestuurder.Rijksregisternummer,
                TyperijbewijsId = efTypeRijbewijs.TypeRijbewijsId
            };
            //Stap 2: het EntityFramework-model toevoegen aan de databank mbv de Context-klasse
            var efBestuurder = _dbContext.Bestuurders.Add(nieuweBestuurder).Entity; //toevoegen
            var count = _dbContext.SaveChanges(); //opslaan, SaveChanges geeft het aantal bewerkte rijen terug, dus als = 1 is de nieuwe bestuurder succesvol toegevoegd
            bestuurder.BestuurderId = efBestuurder.BestuurderId;
            return bestuurder;
        }
        catch (Exception ex)
        {
            //TODO Logging
            throw;
        }

    }


    public void Update(Bestuurder bestuurder) //Bestuurder aanpassen
    {
        // Kijk na of de bestuurder bestaat in de databank, als hij bestaat wordt hij bewerkt    
        if (!Exists(bestuurder))
        {
            return;
        };

        try
        {
            //de nodige EF bestuurder en type rijbewijs ophalen
            var updateBestuurder = GetEFBestuurder(bestuurder);
            var efTypeRijbewijs = _dbContext.TypeRijbewijs.Where(t => t.Type == bestuurder.Rijbewijs).FirstOrDefault();

            //de EF bestuurder aanpassen als de nieuwe gegevens niet leeg zijn
            if (!String.IsNullOrEmpty(bestuurder.Naam))
            {
                updateBestuurder.Naam = bestuurder.Naam;
            };
            if (!String.IsNullOrEmpty(bestuurder.Voornaam))
            {
                updateBestuurder.Voornaam = bestuurder.Voornaam;
            };
            if (!String.IsNullOrEmpty(bestuurder.Adres))
            {
                updateBestuurder.Adres = bestuurder.Adres;
            };
            if (!String.IsNullOrEmpty(bestuurder.Rijksregisternummer))
            {
                updateBestuurder.Rijksregisternummer = bestuurder.Rijksregisternummer;
            };
            if (!String.IsNullOrEmpty(bestuurder.Rijbewijs))
            {
                updateBestuurder.TyperijbewijsId = efTypeRijbewijs.TypeRijbewijsId;
            }

            var efUpdate = _dbContext.Update(updateBestuurder).Entity; // de nieuwe gegevens doorgeven aan EF
            var count = _dbContext.SaveChanges(); //EF: de updates opslaan in de databank
        }
        catch (Exception ex)
        {
            //TODO Later logging toevoegen
            Debug.WriteLine("An exception has occured while updating Bestuurder: ", ex);
            throw;
        };


    }

    public void Delete(Bestuurder bestuurder)
    {
        if (!Exists(bestuurder)) { return; }

        try
        {
            var efBestuurder = GetEFBestuurder(bestuurder);
            var efDelete = _dbContext.Bestuurders.Remove(efBestuurder).Entity;
            var count = _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("An exception has occured while deleting Bestuurder: ", ex);
        }
    }

    private EF_Infrastructure.Models.Bestuurder GetEFBestuurder(Bestuurder bestuurder)
    {
        if (bestuurder.BestuurderId != 0)
        {
            return _dbContext.Bestuurders.Where(b => b.BestuurderId == bestuurder.BestuurderId).FirstOrDefault();
        }
        return _dbContext.Bestuurders.Where(b => b.Rijksregisternummer == bestuurder.Rijksregisternummer).FirstOrDefault();
    }

    #endregion

}
