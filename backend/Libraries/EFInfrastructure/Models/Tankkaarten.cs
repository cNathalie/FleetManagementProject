using System;
using System.Collections.Generic;

namespace EF_Infrastructure.Models;

public partial class Tankkaarten
{
    public int Id { get; set; }

    public int Kaartnummer { get; set; }

    public DateTime Geldigheidsdatum { get; set; }

    public int Pincode { get; set; }

    public string Brandstoffen { get; set; } = null!;

    public bool? Actief { get; set; }

    public virtual ICollection<Fleet> Fleets { get; set; } = new List<Fleet>();
}
