using System;
using System.Collections.Generic;

namespace FM.Infrastructure.EntityFramework.Models;

public partial class UserRefreshToken
{
    public int RefreshTokenId { get; set; }

    public int UserId { get; set; }

    public string RefreshToken { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime AutoTimeCreation { get; set; }

    public DateTime AutoTimeUpdate { get; set; }

    public int AutoUpdateCount { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User User { get; set; } = null!;
}
