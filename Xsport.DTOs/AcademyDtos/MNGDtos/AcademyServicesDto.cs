using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos.MNGDtos
{
    public class AcademyServicesDto
    {
        public long AcademyId { get; set; }
        public List<long> servicesIds { get; set; } = null!;
    }
}
