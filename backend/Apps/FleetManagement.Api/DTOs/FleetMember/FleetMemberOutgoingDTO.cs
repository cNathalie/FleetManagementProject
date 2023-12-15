namespace FleetManagement.Api.DTOs.FleetMember
{
    public class FleetMemberOutgoingDTO
    {
        public int FleetId { get; set; }
        public string? BestuurderNaam { get; set; }
        public string? BestuurderVoornaam { get; set; }
        public int TankaartId { get; set; }
        public string? VoertuigMerkModel { get; set; }
        public string? VoertuigNummerplaat { get; set; }
        public string? VoertuigChassisnummer { get; set; }
    }
}
