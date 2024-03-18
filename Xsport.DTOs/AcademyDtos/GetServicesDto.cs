using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos
{
    public class GetServicesDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public List<GetServicesValuesDto> Values { get; set; } = null!;
    }
    public class GetServicesValuesDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
