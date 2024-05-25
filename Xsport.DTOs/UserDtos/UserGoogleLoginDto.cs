using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.UserDtos
{
    public class UserGoogleLoginDto
    {
        [Required]
        public string FirebaseToken { get; set; } = null!;
        public string? Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
