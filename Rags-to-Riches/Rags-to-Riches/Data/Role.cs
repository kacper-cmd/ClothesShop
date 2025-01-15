using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data;

public partial class Role
{
    [Key]
    public int RolesId { get; set; }

    public string Role1 { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
