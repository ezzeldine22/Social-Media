using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetAllPostSharesDTO
    {
        public string userName { get; set; }
        public string userId { get; set; }
        public string userPicture { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
