using System;
using System.Collections.Generic;

namespace EF_Infrastructure.Models;

public partial class TypeWagen
{
    public int TypeWagenId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Voertuig> Voertuigs { get; set; } = new List<Voertuig>();
}
