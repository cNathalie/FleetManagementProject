using EF_Infrastructure.Context;
using FM_Domain;
using FM_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EF_Repositories;

public class EFFleetRepository : IFMFleetRepository
{
    private readonly FleetManagementDbContext _dbContext;
    private static List<FleetMember> _fleet = new();
    public List<FleetMember> Fleet
    {
        get
        {
            if( _fleet != null ) return _fleet;
            return RefreshFleet();
        }
    }

    public EFFleetRepository(FleetManagementDbContext dbContext)
    {
        _dbContext = dbContext;
        RefreshFleet();
    }

    public void Delete(FleetMember fleetMember)
    {
        if (!Exists(fleetMember)) return;

        var efFleetMember = GetEFEntity(fleetMember);

        try
        {
            var efRemove = _dbContext.Remove(efFleetMember).Entity;
            var count = _dbContext.SaveChanges();
            RefreshFleet();
        }
        catch(Exception ex)
        {
            //TODO logging
            throw;
        }

    }

    public bool Exists(FleetMember fleetMember)
    {
        return GetEFEntity(fleetMember) != null;
    }

    public FleetMember Insert(FleetMember fleetMember)
    {
        if(Exists(fleetMember)) //als deze combo al bestaat komt dat bestaand object terug
        {
            fleetMember.FleetMemberId = GetEFEntity(fleetMember).FleetId;
            return fleetMember;
        }
      
        try
        {
            var efBestuurder = _dbContext.Bestuurders.Where(b => b.Naam == fleetMember.BestuurderNaam && b.Voornaam == fleetMember.BestuurderVoornaam).FirstOrDefault();
            var efTankkaart = _dbContext.Tankkaarten.Where(t => t.TankkaartId == fleetMember.TankkaartId).FirstOrDefault();
            var efVoertuig = _dbContext.Voertuigen.Where(v =>  v.Chassisnummer == fleetMember.VoertuigChassisnummer).FirstOrDefault();

            EF_Infrastructure.Models.Fleet newFleetMember = new()
            {
                BestuurderId = efBestuurder.BestuurderId,
                TankkaartId = efTankkaart.TankkaartId,
                VoertuigId = efVoertuig.VoertuigId
            };

            var efInsert = _dbContext.Fleet.Add(newFleetMember).Entity;
            var count = _dbContext.SaveChanges();
            fleetMember.FleetMemberId = efInsert.FleetId;
            fleetMember.VoertuigMerkModel = efInsert.Voertuig.MerkEnModel;
            RefreshFleet();
            return fleetMember;
        } 
        catch(Exception ex)
        {
            //logging
            throw;
        }
    }

    public List<FleetMember> RefreshFleet()
    {
        _fleet.Clear();
        var dbFleet = _dbContext.Fleet
                                .Include(f => f.Bestuurder)
                                .Include(f => f.Tankkaart)
                                .Include(f => f.Voertuig)
                                .ToList();
        foreach(var fleet in dbFleet)
        {
            var fm = new FleetMember
            {
                FleetMemberId = fleet.FleetId,
                BestuurderNaam = fleet.Bestuurder.Naam,
                BestuurderVoornaam = fleet.Bestuurder.Voornaam,
                TankkaartId = fleet.TankkaartId,
                VoertuigMerkModel = fleet.Voertuig.MerkEnModel,
                VoertuigNummerplaat = fleet.Voertuig.Nummerplaat,
                VoertuigChassisnummer = fleet.Voertuig.Chassisnummer
            };

            _fleet.Add(fm);
        }

        return _fleet;
    }

    public void Update(FleetMember fleetMember)
    {
        if (Exists(fleetMember)) return;

        var efFleetMember = GetEFEntity(fleetMember);

        var efBestuurder = _dbContext.Bestuurders.Where(b => b.Naam == fleetMember.BestuurderNaam && b.Voornaam == fleetMember.BestuurderVoornaam).FirstOrDefault();
        var efTankkaart = _dbContext.Tankkaarten.Where(t => t.TankkaartId == fleetMember.TankkaartId).FirstOrDefault();
        var efVoertuig = _dbContext.Voertuigen.Where(v => v.Chassisnummer == fleetMember.VoertuigChassisnummer).FirstOrDefault();

        try
        {
            efFleetMember.BestuurderId = efBestuurder.BestuurderId;
            efFleetMember.TankkaartId = efTankkaart.TankkaartId;
            efFleetMember.VoertuigId = efVoertuig.VoertuigId;

            var efUpdate = _dbContext.Fleet.Update(efFleetMember).Entity;
            var count = _dbContext.SaveChanges();
            RefreshFleet();
        }
        catch (Exception ex)
        {
            //logging
            throw;
        }
    }

    private EF_Infrastructure.Models.Fleet GetEFEntity(FleetMember fleetMember)
    {
        if(fleetMember.FleetMemberId != 0) //to assist in update & delete
        {
            return _dbContext.Fleet.Where(fm => fm.FleetId == fleetMember.FleetMemberId).FirstOrDefault();
        }

        //to assist in insert
        var efBestuurder = _dbContext.Bestuurders.Where(b => b.Naam == fleetMember.BestuurderNaam && b.Voornaam == fleetMember.BestuurderVoornaam).FirstOrDefault();
        var efTankkaart = _dbContext.Tankkaarten.Where(t => t.TankkaartId == fleetMember.TankkaartId).FirstOrDefault();
        var efVoertuig = _dbContext.Voertuigen.Where(v => v.Chassisnummer == fleetMember.VoertuigChassisnummer).FirstOrDefault();
        return _dbContext.Fleet.Where(f => f.BestuurderId == efBestuurder.BestuurderId && f.TankkaartId == efTankkaart.TankkaartId && f.VoertuigId == efVoertuig.VoertuigId).FirstOrDefault();    
    }
}
