namespace FM_Domain;

public class Login
{
    public int LoginId { get; set; }

    public string Email { get; set; } = null!;

    public string Wachtwoord { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
