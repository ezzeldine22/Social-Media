using System;
using System.Collections.Generic;

namespace API.Domain.Entites;

public partial class Like
{
    public long Id { get; set; }

    public string UserId { get; set; }

    public long PostId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
