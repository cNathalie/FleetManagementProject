﻿using System;
using System.Collections.Generic;

namespace EF_Infrastructure.Models;

public partial class Fleet
{
    public int Id { get; set; }

    public int BestuurderId { get; set; }

    public int TankkaartId { get; set; }

    public int VoertuigId { get; set; }

    public virtual Bestuurder Bestuurder { get; set; } = null!;

    public virtual Tankkaarten Tankkaart { get; set; } = null!;

    public virtual Voertuig Voertuig { get; set; } = null!;
}
