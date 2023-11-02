using EF_Infrastructure.Context;
using FM_Domain;
using System.Diagnostics;

namespace EF_Repositories;

public class EFBestuurderRepository
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
    private List<Bestuurder> RefreshBestuurders() //Alle bestuurders uit de database ophalen en omzetten naar interne domeinmodellen
    {
        _bestuurders = new();
        var dbBestuurders = _dbContext.Bestuurders.ToList();
        foreach (var b in dbBestuurders)
        {
            var bestuurder = new Bestuurder
            {
                Id = b.BestuurderId,
                Naam = b.Naam,
                Voornaam = b.Voornaam,
                Adres = b.Adres,
                Rijksregisternummer = b.Rijksregisternummer,
                TyperijbewijsId = b.TyperijbewijsId,
                Rijbewijs = "Nog in te vullen wanneer andere repo af is" //b.Typerijbewijs.Type
                
            };
            _bestuurders.Add(bestuurder);
        }

        return _bestuurders;
    }

    public void Insert(Bestuurder bestuurder) // een nieuwe bestuurder toevoegen aan de database
    {
        //Stap 1: Omzetten van het interne domein-model naar het EntityFramework-model
        EF_Infrastructure.Models.Bestuurder nieuweBestuurder = new()
        {
            BestuurderId = bestuurder.Id,
            Naam = bestuurder.Naam,
            Voornaam = bestuurder.Voornaam,
            Adres = bestuurder.Adres,
            Rijksregisternummer = bestuurder.Rijksregisternummer,
            TyperijbewijsId = bestuurder.TyperijbewijsId
        };
        //Stap 2: het EntityFramework-model toevoegen aan de databank mbv de Context-klasse
        var efBestuurder = _dbContext.Add(nieuweBestuurder).Entity; //toevoegen
        var count = _dbContext.SaveChanges(); //opslaan, SaveChanges geeft het aantal bewerkte rijen terug, dus als = 1 is de nieuwe bestuurder succesvol toegevoegd

        //Stap 3: als succesvol nieuwe bestuurder toevoegen aan de Repository lijst (!! als domein-model, niet EF)
        if (count == 1)
        {
            _bestuurders.Add(bestuurder);
        }
    }

    public void Update(Bestuurder bestuurder) //Bestuurder aanpassen
    {
        if (!Exists(bestuurder))  // Kijk na of de bestuurder bestaan in de databank, als hij bestaat wordt hij bewerkt        
        {
            //TODO exeption gooien?
            return;
        };

        //de nodige EF bestuurder ophalen
        var updateBestuurder = GetEFBestuurder(bestuurder);

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

        //TODO    als er een rijbewijsrepo is hier iets doen          
        //updateBestuurder.TyperijbewijsId = 

        try
        {
            var efUpdate = _dbContext.Update(updateBestuurder).Entity; // de nieuwe gegevens doorgeven aan EF
            var count = _dbContext.SaveChanges(); //EF: de updates opslaan in de databank

            if (count == 1)
            {
                RefreshBestuurders(); //De repo lijst updaten
            }

        }
        catch (Exception ex)
        {
            //TODO Later logging toevoegen?
            Debug.WriteLine("An exception has occured while updating Bestuurder: ", ex);
        };


    }

    public void Delete(Bestuurder bestuurder)
    {
        if(!Exists(bestuurder)){
            return;
        }

        try{
            var efBestuurder = GetEFBestuurder(bestuurder);
            var efDelete = _dbContext.Bestuurders.Remove(efBestuurder).Entity;
            var count = _dbContext.SaveChanges();    
            if (count == 1){
                RefreshBestuurders();
            }        
        } catch (Exception ex){
            Debug.WriteLine("An exception has occured while deleting Bestuurder: ", ex);
        }


    }


    public bool Exists(Bestuurder bestuurder)
    {
        RefreshBestuurders();
        bool exists = _bestuurders.First(b => b.Id == bestuurder.Id && b.Naam == bestuurder.Naam && b.Rijksregisternummer == bestuurder.Rijksregisternummer) != null; //als er een bestuurder met dezelfde gegevens als de gezochte bestuurder aanwezig is = true
        return exists;
    }



    private EF_Infrastructure.Models.Bestuurder GetEFBestuurder(Bestuurder bestuurder)
    {
        var efBestuurder = _dbContext.Bestuurders.Find(bestuurder.Id);
        return efBestuurder;
    }



}
