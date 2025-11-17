using System;
using System.Collections.Generic;

namespace API.Domain.Entites;

public partial class Follow
{
    public long Id { get; set; }

    public long FollowerId { get; set; }

    public long FollowedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Followed { get; set; } = null!;

    public virtual User Follower { get; set; } = null!;
}
