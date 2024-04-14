using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs
{
    public class DropDownDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public AdditionalInfo? Info { get; set; } = null!;
    }
    public class AdditionalInfo
    {
        public int From { get; set; }
        public int To { get; set; }
    }
}
