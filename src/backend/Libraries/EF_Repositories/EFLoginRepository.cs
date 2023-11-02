using EF_Infrastructure.Context;
using FM_Domain;

namespace EF_Repositories;

public class EFLoginRepository
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
    private List<Login> RefreshLogins()
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
        //Stap 1: Omzetten van het interne domein-model naar het EntityFramework-model
        EF_Infrastructure.Models.Login nieuweBrandstof = new()
        {
            Email = newLogin.Email,
            Wachtwoord = newLogin.Wachtwoord,
        };
        //Stap 2: het EntityFramework-model toevoegen aan de databank mbv de Context-klasse
        var efLogin = _dbContext.Add(newLogin).Entity; //toevoegen
        var count = _dbContext.SaveChanges(); //opslaan, SaveChanges geeft het aantal bewerkte rijen terug

        //Stap 3: als succesvol nieuwe tankkaart toevoegen aan de Repository lijst (!! als domein-model, niet EF)
        if (count == 1)
        {
            _logins.Add(newLogin);
        }
    }

}
