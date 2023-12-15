using System;
using System.Collections.Generic;

namespace FM.Infrastructure.EntityFramework.Models;

public partial class Tankkaart
{
    public int TankkaartId { get; set; }

    public int Kaartnummer { get; set; }

    public DateTime Geldigheidsdatum { get; set; }

    public int Pincode { get; set; }

    public int BrandstofTypeId { get; set; }

    public bool? Actief { get; set; }

    public DateTime AutoTimeCreation { get; set; }

    public DateTime AutoTimeUpdate { get; set; }

    public int AutoUpdateCount { get; set; }

    public bool IsDeleted { get; set; }

    public virtual BrandstofType BrandstofType { get; set; } = null!;

    public virtual ICollection<FleetMember> FleetMembers { get; set; } = new List<FleetMember>();
}
