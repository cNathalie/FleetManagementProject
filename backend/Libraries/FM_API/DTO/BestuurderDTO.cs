using System.ComponentModel.DataAnnotations;

namespace FM_API
{
    public class BestuurderDTO
    {
        public int BestuurderId { get; set; }
        [Required] public string Naam { get; set; } = string.Empty;

        [Required] public string Voornaam { get; set; } = string.Empty;

        [Required] public string Adres { get; set; } = string.Empty;

        [Required] [MinLength(11)][MaxLength(11)] 
        public string Rijksregisternummer { get; set; } = string.Empty;

        [Required] public string Rijbewijs { get; set; }
    }
}
