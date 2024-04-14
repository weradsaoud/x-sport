using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.UserDtos
{
    public class ChangeEmailDto
    {
        [Required]
        public string NewEmail { get; set; } = null!;
    }
}
