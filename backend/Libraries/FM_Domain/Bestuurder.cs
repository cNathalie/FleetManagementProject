using FM_Domain.Exceptions;
using FM_Domain.Validators;

namespace FM_Domain;

public class Bestuurder
{
    public int BestuurderId { get; set; }

    public string Naam { get; set; } = null!;

    public string Voornaam { get; set; } = null!;

    public string Adres { get; set; } = null!;

    private string _rijksregisternummer;
    public string Rijksregisternummer
    {
        get
        {
            return _rijksregisternummer;
        }
        set
        {
            value.Trim();
            if (RRNValidator.CheckRijksRegisterNumberChecksum(value))
            {
                _rijksregisternummer = value;
            }
            else
            {
                throw new RRNException("Invalid RRN");
            }
        }
    }

    public string Rijbewijs { get; set; }
}
    

