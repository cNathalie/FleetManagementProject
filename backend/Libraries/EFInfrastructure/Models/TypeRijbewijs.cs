using System;
using System.Collections.Generic;

namespace EF_Infrastructure.Models;

public partial class TypeRijbewijs
{
    public int TypeRijbewijsId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Bestuurder> Bestuurders { get; set; } = new List<Bestuurder>();
}
