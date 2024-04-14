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
        public string StadiumType { get; set; } = null!;
        public string RegionName { get; set; } = "تضاف لاحقا";
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public int NumOfReviews { get; set; }
        public double Evaluation { get; set; }

        public string CoverPhoto { get; set; } = null!;
        public string CoverVideo { get; set; } = null!;
        public List<string> Photos { get; set; } = null!;
        public List<string> Videos { get; set; } = null!;
    }
}
