using System;
using System.Collections.Generic;

namespace SistemPenitentiary.Models;

public partial class Animation
{
    public Guid AnimationId { get; set; }

    public string AnimationName { get; set; } = null!;

    public string AnimationUrl { get; set; } = null!;

    public string? Description { get; set; }

    public Guid? ProcessId { get; set; }

    public int? Duration { get; set; }

    public string? AnimationType { get; set; }

    public bool? RequiresStudentRole { get; set; }

    public bool? RequiresMaestroRole { get; set; }

    public virtual Process? Process { get; set; }
}
