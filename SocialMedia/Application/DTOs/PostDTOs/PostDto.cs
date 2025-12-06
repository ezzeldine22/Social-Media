using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PostDTOs
{
    public class PostDto
    {
        [Required]
        public string Caption { get; set; }
        public string? Image_Url { get; set; } 
        public string? Video_Url { get; set; } 
    }
}
