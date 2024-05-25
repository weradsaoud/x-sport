using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.StadiumDtos.MNGDtos
{
    public class StadiumWorkingDaysDto
    {
        public long StadiumId { get; set; }
        public List<GeneralWorkingDayDto> StadiumWorkingDays { get; set; } = null!;
    }
    public class GeneralWorkingDayDto
    {
        public long WorkingDayId { get; set; }
        public string OpenAt { get; set; } = null!;
        public string CloseAt { get; set; } = null!;
    }
}
