namespace FM_Domain;

public class Voertuig
{
    public int Id { get; set; }

    public string MerkEnModel { get; set; } = null!;

    public string Chassisnummer { get; set; } = null!;

    public string Nummerplaat { get; set; } = null!;

    public int BrandstoftypeId { get; set; }

    public int TypewagenId { get; set; }

    public string Kleur { get; set; } = null!;

    public int AantalDeuren { get; set; }
}
