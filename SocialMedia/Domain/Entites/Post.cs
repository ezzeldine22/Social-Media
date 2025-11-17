using System;
using System.Collections.Generic;

namespace API.Domain.Entites;

public partial class Post
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string? Caption { get; set; }

    public string? ImageUrl { get; set; }

    public string? VideoUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Share> Shares { get; set; } = new List<Share>();

    public virtual User User { get; set; } = null!;
}
