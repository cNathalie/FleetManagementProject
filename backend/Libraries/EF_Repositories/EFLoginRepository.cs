using EF_Infrastructure.Context;
using FM_Domain;
using FM_Domain.Interfaces;

namespace EF_Repositories;

public class EFLoginRepository : IFMLoginRepository
{
    // Properties
    private readonly FleetManagementDbContext _dbContext;
    private List<Login> _logins;
    public List<Login> Logins
    {
        get
        {
            if (_logins != null) return _logins
    ;
            return RefreshLogins();
        }
    }

    //  Constructor
    public EFLoginRepository(FleetManagementDbContext context)
    {
        _dbContext = context;
        _logins = RefreshLogins();
    }

    // Methodes
    public List<Login> RefreshLogins()
    {
        _logins = new();
        var dbLogins = _dbContext.Logins.ToList();
        foreach (var l in dbLogins)
        {
            var login = new Login
            {
                LoginId = l.LoginId,
                Email = l.Email,
                Wachtwoord = l.Wachtwoord,
                Rol = l.Rol
            };
            _logins.Add(login);
        }

        return _logins
;
    }

    public void Insert(Login newLogin)
    {
        if(Exists(newLogin)) { return; }

        try
        {
            //Stap 1: Omzetten van het interne domein-model naar het EntityFramework-model
            EF_Infrastructure.Models.Login nLogin = new()
            {
                Email = newLogin.Email,
                Wachtwoord = newLogin.Wachtwoord,
                Rol = newLogin.Rol
            };

            //Stap 2: het EntityFramework-model toevoegen aan de databank mbv de Context-klasse
            var efLogin = _dbContext.Add(nLogin).Entity; //toevoegen
            var count = _dbContext.SaveChanges(); //opslaan, SaveChanges geeft het aantal bewerkte rijen terug
            RefreshLogins();
        }
        catch (Exception ex)
        {
            //logging
            throw;
        }

    }

    public void Update(Login login)
    {
        if(!Exists(login)) { return; }

        var efLogin = GetEFEntity(login);

        try
        {
            efLogin.Email = login.Email;
            efLogin.Wachtwoord = login.Wachtwoord;
            efLogin.Rol = login.Rol;

            var efUpdate = _dbContext.Update(efLogin).Entity;
            var count = _dbContext.SaveChanges();
            RefreshLogins();
        }
        catch(Exception ex) 
        { 
            //TODO logging
            throw; 
        }
    }

    public void Delete(Login login)
    {
        if(!Exists(login)) { return; }
        var efLogin = GetEFEntity(login);
        try
        {
            var efRemove = _dbContext.Remove(efLogin).Entity;
            var count = _dbContext.SaveChanges();
            RefreshLogins();
        }
        catch (Exception ex)
        {
            //TODO logging
            throw;
        }
    }

    public string Authenticate(Login login)
    {
        if(!Exists(login))
        {
            return "Onbekende gebruiker";

        }
        
        var efLogin = GetEFEntity(login);
        if(efLogin.Wachtwoord == login.Wachtwoord)
        {
            return efLogin.Rol;
        }

        return "Fout wachtwoord";
    }

    public bool Exists(Login login)
    {
        return GetEFEntity(login) != null;
    }

    private EF_Infrastructure.Models.Login GetEFEntity(Login login)
    {
        if (login.LoginId != 0)
        {
            return _dbContext.Logins.Where(l => l.LoginId == login.LoginId).FirstOrDefault();
        }
        return _dbContext.Logins.Where(l => l.Email == login.Email).FirstOrDefault();
    }
}
