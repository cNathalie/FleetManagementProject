using FleetManagement.Api.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Api.DTOs.Tankkaart
{
    public class TankkaartNewIncomingDTO
    {
        [Required]
        [ExactLengthNumber(9, ErrorMessage = "Must be 9 digits")] 
        public int Kaartnummer { get; set; }

        [Required]
        public DateOnly Geldigheidsdatum { get; set; }

        [Required]
        public string? Brandstoftype { get; set; }

        [Required]
        [ExactLengthNumber(4, ErrorMessage = "Must be 4 digits")] 
        public int Pincode { get; set; }

        [Required] 
        public bool? isActief { get; set; }
    }
}
