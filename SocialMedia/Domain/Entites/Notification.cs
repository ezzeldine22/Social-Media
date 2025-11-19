using System;
using System.Collections.Generic;

namespace API.Domain.Entites;

public partial class Notification
{
    public long Id { get; set; }

    public string UserId { get; set; }

    public string SenderId { get; set; }

    public string Type { get; set; } = null!;

    public long ReferenceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Sender { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
