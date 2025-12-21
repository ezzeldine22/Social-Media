using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetCommentDTO
    {
        public string Text { get; set; }
        public long CommentID { get; set; } 
        public string userPic { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public DateTime? CommmentDateTime { get; set; } 
    }
}
