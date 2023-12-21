namespace FleetManagement.Api.DTOs.Voertuig
{
    public class VoertuigOutgoingDTO
    {
        public int VoertuigId { get; set; }
        public string? MerkEnModel { get; set; }
        public string? Chassisnummer { get; set; }
        public string? Nummerplaat { get; set; }
        public string? BrandstofType { get; set; }
        public string? TypeWagen { get; set; }
        public string? Kleur { get; set; }
        public int AantalDeuren { get; set; }
    }
}
