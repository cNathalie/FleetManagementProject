using System;
using System.Collections.Generic;

namespace EF_Infrastructure.Models;

public partial class Bestuurder
{
    public int BestuurderId { get; set; }

    public string Naam { get; set; } = null!;

    public string Voornaam { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string Rijksregisternummer { get; set; } = null!;

    public int TyperijbewijsId { get; set; }

    public virtual Fleet? Fleet { get; set; }

    public virtual TypeRijbewijs Typerijbewijs { get; set; } = null!;
}
