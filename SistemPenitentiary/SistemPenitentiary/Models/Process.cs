using System;
using System.Collections.Generic;

namespace SistemPenitentiary.Models;

public partial class Process
{
    public Guid ProcessId { get; set; }

    public string ProcessName { get; set; } = null!;

    public string? Description { get; set; }

    public int TotalStages { get; set; }

    public string? Configuration { get; set; }

    public string? UnrealAssetPath { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Animation> Animations { get; set; } = new List<Animation>();

    public virtual ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
}
