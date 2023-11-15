using EF_Infrastructure.Context;
using FM_Domain;
using FM_Domain.Interfaces;
using System.Diagnostics;

namespace EF_Repositories;

public class EFBrandstofTypeRepository : IFMBrandstoftypeRepository
{
    // Properties
    private readonly FleetManagementDbContext _dbContext;
    private List<BrandstofType> _brandstoffen;
    public List<BrandstofType> Brandstoffen
    {
        get
        {
            if (_brandstoffen != null) return _brandstoffen;
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
    public List<BrandstofType> RefreshBrandstoffen() 
    {
        _brandstoffen = new();
        var dbBrandstoffen = _dbContext.BrandstofTypes.ToList();
        foreach (var bt in dbBrandstoffen)
        {
            var brandstofType = new BrandstofType
            {
                BrandstofTypeId = bt.BrandstofTypeId,
                Type = bt.Type,
            };
            _brandstoffen.Add(brandstofType);
        }

        return _brandstoffen
;
    }

    public void Insert(BrandstofType brandstofType)
    {
        //Stap 1: Omzetten van het interne domein-model naar het EntityFramework-model
        EF_Infrastructure.Models.BrandstofType nieuweBrandstof = new()
        {
            BrandstofTypeId = brandstofType.BrandstofTypeId,
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

    public void Update(BrandstofType brandstofType) //Tankkaart aanpassen
    {
        if (!Exists(brandstofType))        
        {
            return;
        };

        //de nodige EF brandstof entiteit ophalen
        var efUpdateBrandstof = GetEFEntity(brandstofType);

        //de EF tankkaart aanpassen
        try
        {
            efUpdateBrandstof.Type = brandstofType.Type;

            var efUpdate = _dbContext.Update(efUpdateBrandstof).Entity; // de nieuwe gegevens doorgeven aan EF
            var count = _dbContext.SaveChanges(); //EF: de updates opslaan in de databank
            RefreshBrandstoffen();
        }
        catch (Exception ex)
        {
            //TODO Later logging toevoegen
            Debug.WriteLine("An exception has occured while updating Brandstof: ", ex);
            throw;
        };


    }

    public void Delete(BrandstofType brandstof)
    {
        if (!Exists(brandstof))
        {
            return;
        }

        var efBrandstof = GetEFEntity(brandstof);
        try
        {
            var efDelete = _dbContext.BrandstofTypes.Remove(efBrandstof).Entity;
            var count = _dbContext.SaveChanges();
            RefreshBrandstoffen();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An exception has occured while deleting Brandstoftype ({efBrandstof.Type}): ", ex);
        }


    }


    public bool Exists(BrandstofType brandstof)
    {
        return GetEFEntity(brandstof) != null;
    }



    private EF_Infrastructure.Models.BrandstofType GetEFEntity(BrandstofType brandstof)
    {
        if(brandstof.BrandstofTypeId != 0)
        {
            return  _dbContext.BrandstofTypes.Where(b =>b.BrandstofTypeId == brandstof.BrandstofTypeId).FirstOrDefault();
        }
        return _dbContext.BrandstofTypes.Where(b => b.Type == brandstof.Type).FirstOrDefault();
    }

}
