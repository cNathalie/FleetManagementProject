using EF_Infrastructure.Context;
using FM_Domain;
using System.Diagnostics;

namespace EF_Repositories;

public class EFBrandstofTypeRepository
{
    // Properties
    private readonly FleetManagementDbContext _dbContext;
    private List<BrandstofType> _brandstoffen;
    public List<BrandstofType> Brandstoffen
    {
        get
        {
            if (_brandstoffen != null) return _brandstoffen
    ;
            return RefreshBrandstoffen();
        }
    }

    //  Constructor
    public EFBrandstofTypeRepository(FleetManagementDbContext context)
    {
        _dbContext = context;
        _brandstoffen = RefreshBrandstoffen(); 
    }

    // Methodes
    private List<BrandstofType> RefreshBrandstoffen() 
    {
        _brandstoffen = new();
        var dbBrandstoffen = _dbContext.BrandstofTypes.ToList();
        foreach (var bt in dbBrandstoffen)
        {
            var brandstofType = new BrandstofType
            {
                Id = bt.BrandstofTypeId,
                Type = bt.Type,
            };
            _brandstoffen.Add(brandstofType);
        }

        return _brandstoffen
;
    }

    public void Insert(BrandstofType brandstofType) // een nieuwe tankkaart toevoegen aan de database
    {
        //Stap 1: Omzetten van het interne domein-model naar het EntityFramework-model
        EF_Infrastructure.Models.BrandstofType nieuweBrandstof = new()
        {
            BrandstofTypeId = brandstofType.Id,
            Type = brandstofType.Type,
        };
        //Stap 2: het EntityFramework-model toevoegen aan de databank mbv de Context-klasse
        var efBrandstofType = _dbContext.Add(nieuweBrandstof).Entity; //toevoegen
        var count = _dbContext.SaveChanges(); //opslaan, SaveChanges geeft het aantal bewerkte rijen terug

        //Stap 3: als succesvol nieuwe tankkaart toevoegen aan de Repository lijst (!! als domein-model, niet EF)
        if (count == 1)
        {
            _brandstoffen.Add(brandstofType);
        }
    }

    //public void Update(Tankkaart tankkaart) //Tankkaart aanpassen
    //{
    //    if (!Exists(tankkaart))  // Kijk na of de tankkaart bestaan in de databank, als hij bestaat wordt hij bewerkt        
    //    {
    //        //TODO exeption gooien?
    //        return;
    //    };

    //    //de nodige EF tankkaart ophalen
    //    var updateTankkaart = GetEFTankkaart(tankkaart);

    //    //de EF tankkaart aanpassen als de nieuwe gegevens niet leeg zijn
    //    if (tankkaart.Kaartnummer != null)
    //    {
    //        updateTankkaart.Kaartnummer = tankkaart.Kaartnummer;
    //    };
    //    if (tankkaart.Geldigheidsdatum != null)
    //    {
    //        updateTankkaart.Geldigheidsdatum = tankkaart.Geldigheidsdatum;
    //    };
    //    if (tankkaart.Pincode != null)
    //    {
    //        updateTankkaart.Pincode = tankkaart.Pincode;
    //    };
    //    if (tankkaart.BrandstofTypeId != null)
    //    {
    //        updateTankkaart.BrandstofType.BrandstofTypeId = tankkaart.BrandstofTypeId;
    //    }
    //    if (tankkaart.Actief != null)
    //    {
    //        updateTankkaart.Actief = tankkaart.Actief;
    //    }



    //    try
    //    {
    //        var efUpdate = _dbContext.Update(updateTankkaart).Entity; // de nieuwe gegevens doorgeven aan EF
    //        var count = _dbContext.SaveChanges(); //EF: de updates opslaan in de databank

    //        if (count == 1)
    //        {
    //            RefreshTankkaarten(); //De repo lijst updaten
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        //TODO Later logging toevoegen?
    //        Debug.WriteLine("An exception has occured while updating Tankaart: ", ex);
    //    };


    //}

    //public void Delete(Tankkaart tankkaart)
    //{
    //    if (!Exists(tankkaart))
    //    {
    //        return;
    //    }

    //    try
    //    {
    //        var efTankkaart = GetEFTankkaart(tankkaart);
    //        var efDelete = _dbContext.Tankkaarten.Remove(efTankkaart).Entity;
    //        var count = _dbContext.SaveChanges();
    //        if (count == 1)
    //        {
    //            RefreshTankkaarten();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine("An exception has occured while deleting Bestuurder: ", ex);
    //    }


    //}


    //public bool Exists(Tankkaart tankkaart)
    //{
    //    RefreshTankkaarten();
    //    bool exists = _tankkaarten.First(t => t.TankkaartId == tankkaart.TankkaartId && t.Kaartnummer == tankkaart.Kaartnummer) != null; //als er een tankkaart met dezelfde gegevens als de gezochte tankkaart aanwezig is = true
    //    return exists;
    //}



    //private EF_Infrastructure.Models.Tankkaart GetEFTankkaart(Tankkaart tankkaart)
    //{
    //    var efTankkaart = _dbContext.Tankkaarten.Find(tankkaart.TankkaartId);
    //    return efTankkaart;
    //}

}
