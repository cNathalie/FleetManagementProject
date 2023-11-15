namespace FM_API.DTO
{
    public class VoertuigDTO
    {
        public int VoertuigId { get; set; }

        public string MerkEnModel { get; set; } = null!;

        public string Chassisnummer { get; set; } = null!;

        public string Nummerplaat { get; set; } = null!;

        public string Brandstoftype { get; set; }

        public string Typewagen { get; set; }

        public string Kleur { get; set; } = null!;

        public int AantalDeuren { get; set; }
    }
}
