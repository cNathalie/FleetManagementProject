using System;
using System.Collections.Generic;

namespace EF_Infrastructure.Models;

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

    public virtual BrandstofType BrandstofType { get; set; } = null!;

    public virtual Fleet Fleet { get; set; }

    public virtual TypeWagen TypeWagen { get; set; } = null!;
}
