using System;
using System.Collections.Generic;

namespace EFInfrastructure.Models;

public partial class TypeRijbewijs
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Bestuurder> Bestuurders { get; set; } = new List<Bestuurder>();
}
