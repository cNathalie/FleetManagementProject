using System;
using System.Collections.Generic;

namespace FM.Infrastructure.EntityFramework.Models;

public partial class FleetMember
{
    public int FleetId { get; set; }

    public int BestuurderId { get; set; }

    public int TankkaartId { get; set; }

    public int VoertuigId { get; set; }

    public DateTime AutoTimeCreation { get; set; }

    public DateTime AutoTimeUpdate { get; set; }

    public int AutoUpdateCount { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Bestuurder Bestuurder { get; set; } = null!;

    public virtual Tankkaart Tankkaart { get; set; } = null!;

    public virtual Voertuig Voertuig { get; set; } = null!;
}
