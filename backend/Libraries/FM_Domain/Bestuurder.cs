namespace FM_Domain;

public class Bestuurder
{

    public int Id { get; set; }

    public string Naam { get; set; } = null!;

    public string Voornaam { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string Rijksregisternummer { get; set; } = null!;

    public int TyperijbewijsId { get; set; }

    public string Rijbewijs {  get; set; }
    
}
