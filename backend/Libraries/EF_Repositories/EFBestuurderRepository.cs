using EF_Infrastructure.Context;
using FM_Domain;
using FM_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EF_Repositories;

public class EFBestuurderRepository : IFMBestuurderRepository
{
    // Properties
    private readonly FleetManagementDbContext _dbContext;
    private List<Bestuurder> _bestuurders;
    public List<Bestuurder> Bestuurders
    {
        get
        {
            if (_bestuurders != null) return _bestuurders;
            return RefreshBestuurders();
        }
    }

    //  Constructor
    public EFBestuurderRepository(FleetManagementDbContext context)
    {
        _dbContext = context;
        RefreshBestuurders(); // bij het aanmaken van de Repo wordt het _bestuurders property ingevuld
    }

    // Methodes
    public List<Bestuurder> RefreshBestuurders() //Alle bestuurders uit de database ophalen en omzetten naar interne domeinmodellen
    {
        _bestuurders = new();
        var dbBestuurders = _dbContext.Bestuurders.ToList();
        var dbTypeRijbewijzen = _dbContext.TypeRijbewijs.ToList();
        foreach (var b in dbBestuurders)
        {
            var bestuurder = new Bestuurder
            {
                BestuurderId = b.BestuurderId,
                Naam = b.Naam,
                Voornaam = b.Voornaam,
                Adres = b.Adres,
                Rijksregisternummer = b.Rijksregisternummer,
                Rijbewijs = dbTypeRijbewijzen.Where(t => t.TypeRijbewijsId == b.TyperijbewijsId).FirstOrDefault().Type

            };
            _bestuurders.Add(bestuurder);
        }

        return _bestuurders;
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
            RefreshBestuurders();
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
            RefreshBestuurders(); //De repo lijst updaten
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
            if (count == 1)
            {
                RefreshBestuurders();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("An exception has occured while deleting Bestuurder: ", ex);
        }
    }

    public bool Exists(Bestuurder bestuurder)
    {
        return GetEFBestuurder(bestuurder) != null;
    }

    private EF_Infrastructure.Models.Bestuurder GetEFBestuurder(Bestuurder bestuurder)
    {
        if (bestuurder.BestuurderId != 0)
        {
            return _dbContext.Bestuurders.Where(b => b.BestuurderId == bestuurder.BestuurderId).FirstOrDefault();
        }
        return _dbContext.Bestuurders.Where(b => b.Rijksregisternummer == bestuurder.Rijksregisternummer).FirstOrDefault();
    }



}
