using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.UserDtos
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; } = null!;
    }
    public class ResetPassworTokendDto
    {
        public string ResetPassworTokend { get; set; } = null!;
    }
}
