using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Models;

namespace Xsport.DTOs.UserDtos
{
    public class ConfirmUserEmailRespDto
    {
        public AuthResult AuthResult { get; set; } = null!;
        public List<SportDto> Sports { get; set; } = null!;
    }
}
