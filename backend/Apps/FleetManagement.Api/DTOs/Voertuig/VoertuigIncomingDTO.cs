using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Api.DTOs.Voertuig
{
    public class VoertuigIncomingDTO
    {
        [Required]
        [MinLength(4)]
        public string? MerkEnModel { get; set; }

        [Required]
        [MinLength(17)]
        public string? Chassisnummer { get; set; }

        [Required]
        [MinLength(5)]
        public string? Nummerplaat { get; set; }

        [Required]
        [MinLength(4)]
        public string? BrandstofType { get; set; }

        [Required]
        [MinLength(4)]
        public string? TypeWagen { get; set; }

        [Required]
        [MinLength(3)]
        public string? Kleur { get; set; }

        [Required]
        [Range(3, 5)]
        public int AantalDeuren { get; set; }
    }
}
