using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos
{
    public class AddServiceToAcademyDto
    {
        public long AcademyId { get; set; }
        public int ServiceValueId { get; set; }
    }
}
