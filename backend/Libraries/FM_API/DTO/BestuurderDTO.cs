namespace FM_API
{
    public class BestuurderDTO
    {
        public int Id { get; set; }

        public string Naam { get; set; } = string.Empty;

        public string Voornaam { get; set; } = string.Empty;

        public string Adres { get; set; } = string.Empty;

        public string Rijksregisternummer { get; set; } = string.Empty;

        public int TyperijbewijsId { get; set; }

        public string Rijbewijs { get; set; }
    }
}
