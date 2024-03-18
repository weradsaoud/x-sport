using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos
{
    public class AboutAcademyDto
    {
        public long AcademyId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Phone { get; set; } = null!;
        public TimeOnly OpenAt { get; set; }
        public TimeOnly CloseAt { get; set; }
        public IEnumerable<ServiceDto> services { get; set; } = null!;
    }
    public class ServiceDto
    {
        public long ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public long ValueId { get; set; }
        public string ValueName { get; set; } = null!;
    }
}
