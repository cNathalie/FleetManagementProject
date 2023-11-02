namespace FM_Domain;

public class Tankkaart
{
    public int TankkaartId { get; set; }

    public int Kaartnummer { get; set; }

    public DateTime Geldigheidsdatum { get; set; }

    public int Pincode { get; set; }

    public int BrandstofTypeId { get; set; } 

    public string Brandstof { get; set; }

    public bool? Actief { get; set; }
}
