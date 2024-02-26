using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Models;

namespace Xsport.DTOs.UserDtos
{
    public class RegisterResponseDto
    {
        public List<SportDto> Sports { get; set; } = null!;
        public AuthResult AuthResult { get; set; } = null!;
    }
}
