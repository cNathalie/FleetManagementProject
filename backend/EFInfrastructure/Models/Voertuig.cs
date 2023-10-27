using System;
using System.Collections.Generic;

namespace EFInfrastructure.Models;

public partial class Voertuig
{
    public int Id { get; set; }

    public string MerkEnModel { get; set; } = null!;

    public string Chassisnummer { get; set; } = null!;

    public string Nummerplaat { get; set; } = null!;

    public int BrandstoftypeId { get; set; }

    public int TypewagenId { get; set; }

    public string Kleur { get; set; } = null!;

    public int AantalDeuren { get; set; }

    public virtual BrantstofType Brandstoftype { get; set; } = null!;

    public virtual ICollection<Fleet> Fleets { get; set; } = new List<Fleet>();

    public virtual TypeWagen Typewagen { get; set; } = null!;
}
