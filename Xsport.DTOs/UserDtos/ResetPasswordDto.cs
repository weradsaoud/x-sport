using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.UserDtos
{
    public class ResetPasswordDto
    {

        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string ResetPassworToken { get; set; } = null!;
        [Required]
        public string Code { get; set; } = null!;
        [Required]
        public string NewPassword { get; set; } = null!;
    }
}
