using EF_Infrastructure.Context;
using FM_Domain;
using FM_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EF_Repositories;

public class EFTankkaartRepository : IFMTankkaartRepository
{
    // Properties
    private readonly FleetManagementDbContext _dbContext;
    private readonly EFBrandstofTypeRepository _brandstoftypeRepository;
    private List<Tankkaart> _tankkaarten;
    public List<Tankkaart> Tankkaarten
    {
        get
        {
            if (_tankkaarten != null) return _tankkaarten;
            return RefreshTankkaarten();
        }
    }

    //  Constructor
    public EFTankkaartRepository(FleetManagementDbContext context, EFBrandstofTypeRepository brandstofRepo)
    {
        _dbContext = context;
        _brandstoftypeRepository = brandstofRepo;
        _tankkaarten = RefreshTankkaarten(); // bij het aanmaken van de Repo wordt het _tankkaarten property ingevuld
    }

    // Methodes
    public List<Tankkaart> RefreshTankkaarten() //Alle tankkaarten uit de database ophalen en omzetten naar interne domeinmodellen
    {
        _tankkaarten = new();
        var dbTankkaarten = _dbContext.Tankkaarten.Include(tk => tk.BrandstofType).ToList();
        foreach (var t in dbTankkaarten)
        {
            var tankkaart = new Tankkaart
            {
                TankkaartId = t.TankkaartId,
                Kaartnummer = t.Kaartnummer,
                IsActief = t.Actief,
                Pincode = t.Pincode,
                Brandstoftype = t.BrandstofType.Type,
                Geldigheidsdatum = t.Geldigheidsdatum,
            };
            _tankkaarten.Add(tankkaart);
        }

        return _tankkaarten
;
    }

    public void Insert(Tankkaart tankkaart) // een nieuwe tankkaart toevoegen aan de database
    {
        var efBrandstof = _dbContext.BrandstofTypes.Where(b => b.Type == tankkaart.Brandstoftype).FirstOrDefault();

        try
        {
            //Stap 1: Omzetten van het interne domein-model naar het EntityFramework-model
            EF_Infrastructure.Models.Tankkaart nieuweTankkaart = new()
            {
                Kaartnummer = tankkaart.Kaartnummer,
                Geldigheidsdatum = tankkaart.Geldigheidsdatum,
                Pincode = tankkaart.Pincode,
                BrandstofTypeId = efBrandstof.BrandstofTypeId
            };
            //Stap 2: het EntityFramework-model toevoegen aan de databank mbv de Context-klasse
            var efTankkaart = _dbContext.Add(nieuweTankkaart).Entity; //toevoegen
            var count = _dbContext.SaveChanges(); //opslaan, SaveChanges geeft het aantal bewerkte rijen terug, dus als = 1 is de nieuwe tankkaart succesvol toegevoegd
            RefreshTankkaarten();
        }
        catch (Exception ex)
        {
            // TODO logging
            throw;
        }
    }

    public void Update(Tankkaart tankkaart) //Tankkaart aanpassen
    {
        if (!Exists(tankkaart))  // Kijk na of de tankkaart bestaan in de databank, als hij bestaat wordt hij bewerkt        
        {
            //TODO exeption gooien?
            return;
        };

        //de nodige EF tankkaart ophalen
        var updateTankkaart = GetEFTankkaart(tankkaart);
        var efBrandstof = _dbContext.BrandstofTypes.Where(b => b.Type == tankkaart.Brandstoftype).FirstOrDefault();


        try
        {
            //de EF tankkaart aanpassen als de nieuwe gegevens niet leeg zijn
            if (tankkaart.Kaartnummer != null)
            {
                updateTankkaart.Kaartnummer = tankkaart.Kaartnummer;
            };
            if (tankkaart.Geldigheidsdatum != null)
            {
                updateTankkaart.Geldigheidsdatum = tankkaart.Geldigheidsdatum;
            };
            if (tankkaart.Pincode != null)
            {
                updateTankkaart.Pincode = tankkaart.Pincode;
            };
            if (tankkaart.Brandstoftype != null)
            {
                updateTankkaart.BrandstofType.BrandstofTypeId = efBrandstof.BrandstofTypeId;
            }
            if (tankkaart.IsActief != null)
            {
                updateTankkaart.Actief = tankkaart.IsActief;
            }

            var efUpdate = _dbContext.Update(updateTankkaart).Entity; // de nieuwe gegevens doorgeven aan EF
            var count = _dbContext.SaveChanges(); //EF: de updates opslaan in de databank

            if (count == 1)
            {
                RefreshTankkaarten(); //De repo lijst updaten
            }

        }
        catch (Exception ex)
        {
            //TODO Later logging toevoegen?
            Debug.WriteLine("An exception has occured while updating Tankaart: ", ex);
        };


    }

    public void Delete(Tankkaart tankkaart)
    {
        if (!Exists(tankkaart))
        {
            return;
        }

        try
        {
            var efTankkaart = GetEFTankkaart(tankkaart);
            var efDelete = _dbContext.Tankkaarten.Remove(efTankkaart).Entity;
            var count = _dbContext.SaveChanges();
            RefreshTankkaarten();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("An exception has occured while deleting Bestuurder: ", ex);
            throw;
        }
    }

    public bool Exists(Tankkaart tankkaart)
    {
        RefreshTankkaarten();
        bool exists = _tankkaarten.First(t => t.TankkaartId == tankkaart.TankkaartId && t.Kaartnummer == tankkaart.Kaartnummer) != null; //als er een tankkaart met dezelfde gegevens als de gezochte tankkaart aanwezig is = true
        return exists;
    }

    private EF_Infrastructure.Models.Tankkaart GetEFTankkaart(Tankkaart tankkaart)
    {
        if (tankkaart.TankkaartId != 0)
        {
            return _dbContext.Tankkaarten.Where(t => t.TankkaartId == tankkaart.TankkaartId).FirstOrDefault();
        }
        return _dbContext.Tankkaarten.Where(t => t.Kaartnummer == tankkaart.Kaartnummer).FirstOrDefault();
    }

}
