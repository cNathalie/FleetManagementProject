using FM_Domain.Enums;

namespace FM_Domain;

public class Login
{
    public int LoginId { get; set; }

    public string Email { get; set; } = null!;

    public string Wachtwoord { get; set; } = null!;

    private FMRole _role;
    public FMRole Rol
    {
        get
        {
            return _role;
        }
        set
        {
            if(Enum.IsDefined(typeof(FMRole), value))
            {
                _role = value;
            }
            else
            {
                _role = FMRole.None;
            }
        }

    }
}
