using System;
using System.Collections.Generic;

namespace EF_Infrastructure.Models;

public partial class BrandstofType
{
    public int BrandstofTypeId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Tankkaart> Tankkaarts { get; set; } = new List<Tankkaart>();

    public virtual ICollection<Voertuig> Voertuigs { get; set; } = new List<Voertuig>();
}
