using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetUserProfileDTO
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string Password { get; set; } = null!;

        public string? Bio { get; set; }

        public string? Pic { get; set; }
    }
}
