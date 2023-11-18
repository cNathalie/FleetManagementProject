using EF_Infrastructure.Context;
using FM_Domain;
using FM_Domain.Enums;
using FM_Domain.Interfaces;
using System.Reflection.Metadata.Ecma335;

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
            FMRole rol;
            if(Enum.TryParse(l.Rol.Trim(), out FMRole enumRole))
            {
                rol = enumRole;
            }
            else
            {
                rol = FMRole.None;
            }

            var login = new Login
            {
                LoginId = l.LoginId,
                Email = l.Email,
                Wachtwoord = l.Wachtwoord,
                Rol = rol
            };
            _logins.Add(login);
        }

        return _logins
;
    }

    public Login Insert(Login newLogin)
    {
        if(Exists(newLogin)) 
        {
            newLogin.LoginId = GetEFEntity(newLogin).LoginId;
            return newLogin;
        }

        try
        {
            //Stap 1: Omzetten van het interne domein-model naar het EntityFramework-model
            EF_Infrastructure.Models.Login nLogin = new()
            {
                Email = newLogin.Email,
                Wachtwoord = newLogin.Wachtwoord,
                Rol = newLogin.Rol.ToString(),
            };

            //Stap 2: het EntityFramework-model toevoegen aan de databank mbv de Context-klasse
            var efLogin = _dbContext.Logins.Add(nLogin).Entity; //toevoegen
            var count = _dbContext.SaveChanges(); //opslaan, SaveChanges geeft het aantal bewerkte rijen terug
            newLogin.LoginId = efLogin.LoginId;
            RefreshLogins();
            return newLogin;
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
            efLogin.Rol = login.Rol.ToString();

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
            var efRemove = _dbContext.Logins.Remove(efLogin).Entity;
            var count = _dbContext.SaveChanges();
            RefreshLogins();
        }
        catch (Exception ex)
        {
            //TODO logging
            throw;
        }
    }

    public FMRole Authenticate(Login login)
    {
        if(!Exists(login))
        {
            return FMRole.None;

        }
        
        var efLogin = GetEFEntity(login);
        if(efLogin.Wachtwoord == login.Wachtwoord)
        {
            return (FMRole)Enum.Parse(typeof (FMRole), efLogin.Rol);
        }

        //Email exists but wrong password
        return FMRole.None;
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
