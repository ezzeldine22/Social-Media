using System;
using System.Collections.Generic;

namespace API.Domain.Entites;

public partial class Comment
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long PostId { get; set; }

    public string Text { get; set; } = null!;

    public long? ParentCommentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Comment? ParentComment { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
