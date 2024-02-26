using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.Common.Models
{
    public class AuthResult
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = null!;
        public bool Is2StepVerificationRequired { get; set; }
        public string Provider { get; set; } = null!;
        public List<string> RandomCodes { get; set; } = new List<string>();
    }
}
