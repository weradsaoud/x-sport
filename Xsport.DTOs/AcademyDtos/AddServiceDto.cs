using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos
{
    public class AddServiceDto
    {
        public string ArName { get; set; } = null!;
        public string EnName { get; set; } = null!;
        public List<AddServiceValueDto> Values { get; set; } = null!;
    }
}
