using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Models;

namespace Xsport.DTOs.UserDtos
{
    public class LoginResponseDto
    {
        public AuthResult AuthResult { get; set; } = null!;
        public UserProfileDto? UserProfile { get; set; } = null!;
    }
}
