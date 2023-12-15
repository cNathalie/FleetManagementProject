using System;
using System.Collections.Generic;

namespace FM.Infrastructure.EntityFramework.Models;

public partial class Voertuig
{
    public int VoertuigId { get; set; }

    public string MerkEnModel { get; set; } = null!;

    public string Chassisnummer { get; set; } = null!;

    public string Nummerplaat { get; set; } = null!;

    public int BrandstofTypeId { get; set; }

    public int TypeWagenId { get; set; }

    public string Kleur { get; set; } = null!;

    public int AantalDeuren { get; set; }

    public DateTime AutoTimeCreation { get; set; }

    public DateTime AutoTimeUpdate { get; set; }

    public int AutoUpdateCount { get; set; }

    public bool IsDeleted { get; set; }

    public virtual BrandstofType BrandstofType { get; set; } = null!;

    public virtual ICollection<FleetMember> FleetMembers { get; set; } = new List<FleetMember>();

    public virtual TypeWagen TypeWagen { get; set; } = null!;
}
