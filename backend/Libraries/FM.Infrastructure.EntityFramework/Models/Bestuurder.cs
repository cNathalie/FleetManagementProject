using System;
using System.Collections.Generic;

namespace FM.Infrastructure.EntityFramework.Models;

public partial class Bestuurder
{
    public int BestuurderId { get; set; }

    public string Naam { get; set; } = null!;

    public string Voornaam { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string Rijksregisternummer { get; set; } = null!;

    public int TyperijbewijsId { get; set; }

    public DateTime AutoTimeCreation { get; set; }

    public DateTime AutoTimeUpdate { get; set; }

    public int AutoUpdateCount { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<FleetMember> FleetMembers { get; set; } = new List<FleetMember>();

    public virtual TypeRijbewijs Typerijbewijs { get; set; } = null!;
}
