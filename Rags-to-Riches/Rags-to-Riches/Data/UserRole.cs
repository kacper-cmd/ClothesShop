using System;
using System.Collections.Generic;

namespace Data;

public partial class UserRole
{
    public int UserRolesId { get; set; }

    public int UserId { get; set; }

    public int RolesId { get; set; }

    public virtual Role Roles { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
