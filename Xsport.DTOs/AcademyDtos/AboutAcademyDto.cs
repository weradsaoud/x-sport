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
        public string RegionName { get; set; } = "تضاف لاحقا";
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Phone { get; set; } = null!;
        public string OpenAt { get; set; } = null!;
        public string CloseAt { get; set; } = null!;
        public IEnumerable<ServiceDto1> services { get; set; } = null!;
    }
    public class ServiceDto1
    {
        public long ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string ImgUrl { get; set; }
    }
}
