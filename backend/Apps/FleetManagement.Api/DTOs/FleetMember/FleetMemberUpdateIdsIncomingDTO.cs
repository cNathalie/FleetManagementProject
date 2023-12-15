using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Api.DTOs.FleetMember
{
    public class FleetMemberUpdateIdsIncomingDTO
    {
        [Required] public int FleetId { get; set; }
        [Required] public int BestuurderId { get; set; }
        [Required] public int VoertuigId { get; set; }
        [Required] public int TankkaartId { get; set; }

    }
}
