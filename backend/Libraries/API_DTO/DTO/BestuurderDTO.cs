using System.ComponentModel.DataAnnotations;

namespace API_DTO
{
    public class BestuurderDTO
    {
        public int BestuurderId { get; set; }
        [Required] public string Naam { get; set; } = string.Empty;

        [Required] public string Voornaam { get; set; } = string.Empty;

        [Required] public string Adres { get; set; } = string.Empty;

        [Required] [StringLength(11)] 
        public string Rijksregisternummer { get; set; } = string.Empty;

        [Required] public string Rijbewijs { get; set; }
    }
}
