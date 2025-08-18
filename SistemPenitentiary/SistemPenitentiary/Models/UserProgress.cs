using System;
using System.Collections.Generic;

namespace SistemPenitentiary.Models;

public partial class UserProgress
{
    public Guid ProgressId { get; set; }

    public Guid UserId { get; set; }

    public Guid ProcessId { get; set; }

    public string CurrentStage { get; set; } = null!;

    public int ProcessPercentage { get; set; }

    public string? GameSpecificData { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public virtual Process Process { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
