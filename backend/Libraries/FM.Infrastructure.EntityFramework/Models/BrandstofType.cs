using System;
using System.Collections.Generic;

namespace FM.Infrastructure.EntityFramework.Models;

public partial class BrandstofType
{
    public int BrandstofTypeId { get; set; }

    public string Type { get; set; } = null!;

    public DateTime AutoTimeCreation { get; set; }

    public DateTime AutoTimeUpdate { get; set; }

    public int AutoUpdateCount { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Tankkaart> Tankkaarts { get; set; } = new List<Tankkaart>();

    public virtual ICollection<Voertuig> Voertuigs { get; set; } = new List<Voertuig>();
}
