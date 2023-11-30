using System.ComponentModel.DataAnnotations;

namespace API_DTO
{
    public class VoertuigDTO
    {
        public int VoertuigId { get; set; }
        [Required] public string MerkEnModel { get; set; } = null!;

        [Required] 
        [StringLength(17)] 
        public string Chassisnummer { get; set; } = null!;

        [Required]
        [RegularExpression(@"^\d-[A-Z]{3}-\d{3}$", ErrorMessage = "The format should be 1-ABC-123")] 
        public string Nummerplaat { get; set; } = null!;

        [Required] public string Brandstoftype { get; set; }
        [Required] public string Typewagen { get; set; }
        [Required] public string Kleur { get; set; } = null!;

        [Required] [Range(2,5)] 
        public int AantalDeuren { get; set; }
    }
}
