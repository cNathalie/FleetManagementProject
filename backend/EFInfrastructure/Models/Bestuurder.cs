using System;
using System.Collections.Generic;

namespace EFInfrastructure.Models;

public partial class Bestuurder
{
    public int Id { get; set; }

    public string Naam { get; set; } = null!;

    public string Voornaam { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string Rijksregisternummer { get; set; } = null!;

    public int TyperijbewijsId { get; set; }

    public virtual ICollection<Fleet> Fleets { get; set; } = new List<Fleet>();

    public virtual TypeRijbewij Typerijbewijs { get; set; } = null!;
}
