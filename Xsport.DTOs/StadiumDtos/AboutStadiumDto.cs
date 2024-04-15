using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.StadiumDtos
{
    public class AboutStadiumDto
    {
        public long StadiumId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<DropDownDto> Floors { get; set; } = null!;
        public string RegionName { get; set; } = "تضاف لاحقا";
        public decimal Price { get; set; }
        public string OpenAt { get; set; } = null!;
        public string CloseAt { get; set; } = null!;
        public List<StadiumService> Services { get; set; } = null!;
        public string CoverPhoto { get; set; } = null!;
        public string CoverVideo { get; set; } = null!;
        public List<string> Photos { get; set; } = null!;
        public List<string> Videos { get; set; } = null!;
    }

    public class StadiumService
    {
        public long ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
    }
}
