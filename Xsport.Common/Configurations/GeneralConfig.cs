using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.Common.Configurations
{
    public class GeneralConfig
    {
        public bool EnableTwoFactor { get; set; }
        public string UIDomain { get; set; } = null!;
        public string BackendDomain { get; set; } = null!;
    }
}
