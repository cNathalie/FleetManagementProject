namespace FleetManagement.Api.DTOs.Bestuurder
{
    public class BestuurderOutgoingDTO
    {
        public int BestuurderId { get; set; }
        public string? Naam { get; set; }
        public string? Voornaam { get; set; }
        public string? Adres { get; set; }
        public string? Rijksregisternummer { get; set; }
        public string? Rijbewijs { get; set; }
    }
}
