using System;
using System.Collections.Generic;

namespace EF_Infrastructure.Models;

public partial class Tankkaart
{
    public int TankkaartId { get; set; }

    public int Kaartnummer { get; set; }

    public DateTime Geldigheidsdatum { get; set; }

    public int Pincode { get; set; }

    public int BrandstofTypeId { get; set; }

    public bool? Actief { get; set; }

    public virtual BrandstofType BrandstofType { get; set; } = null!;

    public virtual Fleet Fleet { get; set; }
}
