using System;
using System.Collections.Generic;

namespace SistemPenitentiary.Models;

public partial class Notification
{
    public Guid NotificationId { get; set; }

    public Guid UserId { get; set; }

    public Guid? TargetUserId { get; set; }

    public string NotificationType { get; set; } = null!;

    public string? NotificationData { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Delivered { get; set; }

    public virtual User? TargetUser { get; set; }

    public virtual User User { get; set; } = null!;
}
