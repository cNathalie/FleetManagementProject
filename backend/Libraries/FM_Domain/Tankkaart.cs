namespace FM_Domain;

public class Tankkaart
{
    public int Id { get; set; }

    public int Kaartnummer { get; set; }

    public DateTime Geldigheidsdatum { get; set; }

    public int Pincode { get; set; }

    public string Brandstoffen { get; set; } = null!;

    public bool? Actief { get; set; }
}
