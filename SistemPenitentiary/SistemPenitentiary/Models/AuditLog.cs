using System;
using System.Collections.Generic;

namespace SistemPenitentiary.Models;

public partial class AuditLog
{
    public Guid LogId { get; set; }

    public Guid UserId { get; set; }

    public string ActionType { get; set; } = null!;

    public string? ActionDetails { get; set; }

    public DateTime? ActionTime { get; set; }

    public virtual User User { get; set; } = null!;
}
