using System;
using System.Collections.Generic;

namespace SistemPenitentiary.Models;

public partial class Environment
{
    public Guid EnvironmentId { get; set; }

    public string EnvironmentName { get; set; } = null!;

    public string UnrealLevelPath { get; set; } = null!;

    public string? ConfigurationSettings { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public Guid? ResponsibleUserId { get; set; }

    public virtual User? ResponsibleUser { get; set; }
}
