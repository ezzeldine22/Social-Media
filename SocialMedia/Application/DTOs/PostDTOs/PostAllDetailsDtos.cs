using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PostDTOs
{
    public class PostAllDetailsDtos
    {
        public long  PostId{ get; set; }
        public string Caption { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime? date { get; set; }

    }
}
