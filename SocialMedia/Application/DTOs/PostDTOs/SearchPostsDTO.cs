using API.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PostDTOs
{
    public class SearchPostsDTO
    {
        public string userName { get; set; }
        public string userPic { get; set; }
        public string? Caption { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Share> Shares { get; set; } = new List<Share>();
    }
}
