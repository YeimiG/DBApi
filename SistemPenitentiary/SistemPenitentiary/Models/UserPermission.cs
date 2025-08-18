using System;
using System.Collections.Generic;

namespace SistemPenitentiary.Models;

public partial class UserPermission
{
    public Guid UserId { get; set; }

    public Guid PermissionId { get; set; }

    public DateTime? GrantedAt { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
