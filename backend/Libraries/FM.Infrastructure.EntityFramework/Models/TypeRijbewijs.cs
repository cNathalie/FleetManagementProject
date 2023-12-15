using System;
using System.Collections.Generic;

namespace FM.Infrastructure.EntityFramework.Models;

public partial class TypeRijbewijs
{
    public int TypeRijbewijsId { get; set; }

    public string Type { get; set; } = null!;

    public DateTime AutoTimeCreation { get; set; }

    public DateTime AutoTimeUpdate { get; set; }

    public int AutoUpdateCount { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Bestuurder> Bestuurders { get; set; } = new List<Bestuurder>();
}
