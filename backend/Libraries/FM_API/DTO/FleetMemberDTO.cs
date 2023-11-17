using System.ComponentModel.DataAnnotations;

namespace FM_API.DTO
{
    public class FleetMemberDTO
    {
        public int FleetMemberId { get; set; }
        [Required] public string BestuurderNaam { get; set; }
        [Required] public string BestuurderVoornaam { get; set; }
        [Required] public int TankkaartId { get; set; }
        public string VoertuigMerkModel { get; set; }
        public string VoertuigNummerplaat { get; set; }
        [Required] public string VoertuigChassisnummer { get; set; }
    }
}
