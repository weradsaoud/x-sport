using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.StadiumDtos
{
    public class SuggestedStadiumDto
    {
        public long StadiumId { get; set; }
        public string StadiumName { get; set; } = null!;
        public List<SuggestedStadiumFloorDto> Floors { get; set; } = null!;
        public string RegionName { get; set; } = "تضاف لاحقا";
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public int NumOfReviews { get; set; }
        public double Evaluation { get; set; }
        public List<StadiumWorkDay> StadiumWorkDays { get; set; } = null!;

        public string CoverPhoto { get; set; } = null!;
        public string CoverVideo { get; set; } = null!;
        public List<string> Photos { get; set; } = null!;
        public List<string> Videos { get; set; } = null!;
    }
    public class SuggestedStadiumFloorDto
    {
        public long FloorId { get; set; }
        public long SportId { get; set; }
        public string SportName { get; set; } = null!;
        public string FloorName { get; set; } = null!;
        public int NumPlayers { get; set; }
    }
    public class StadiumWorkDay
    {
        public int DayOrder { get; set; }
        public string DayName { get; set; } = null!;
        public string OpenAt { get; set; } = null!;
        public string CloseAt { get; set; } = null!;
    }
}
