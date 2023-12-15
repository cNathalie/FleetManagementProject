using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Api.DTOs.Bestuurder
{
    public class BestuurderIncomingDTO
    {
        [Required][MinLength(2)] 
        public string? Naam { get; set; }

        [Required][MinLength(2)] 
        public string? Voornaam { get; set; }

        [Required] 
        public string? Adres { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "Must be exactly 11 characters long which can be letters or digits")]
        public string? Rijksregisternummer { get; set; }

        [Required]
        [RegularExpression(@"^Type\s[A-Z\d]{1,3}$", ErrorMessage = "Must be in the format 'Type X', where X is up to 3 characters, which can be capital letters or digits.")]
        public string? Rijbewijs { get; set; }
    }
}
