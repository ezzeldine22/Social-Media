using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AccountDTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email is Required")]
        [MaxLength(50), EmailAddress(ErrorMessage ="invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="password is required ")]
        [MinLength(6 , ErrorMessage ="minmum lehgth of passsword is 6 ")]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
