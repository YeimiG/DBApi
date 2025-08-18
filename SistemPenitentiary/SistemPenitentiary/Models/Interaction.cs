using System;
using System.Collections.Generic;

namespace SistemPenitentiary.Models;

public partial class Interaction
{
    public Guid InteractionId { get; set; }

    public Guid UserId { get; set; }

    public DateTime? InteractionTime { get; set; }

    public string InteractionType { get; set; } = null!;

    public string? InteractionDetails { get; set; }

    public virtual User User { get; set; } = null!;
}
