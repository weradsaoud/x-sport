using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.StadiumDtos.MNGDtos;

namespace Xsport.DTOs.AcademyDtos.MNGDtos
{
    public class AcademyWorkingDaysDto
    {
        public long AcademyId { get; set; }
        public List<GeneralWorkingDayDto> AcademyWorkingDays { get; set; } = null!;
    }
    //public class AcademyWorkingDayDto
    //{
    //    public long WorkingDayId { get; set; }
    //    public string OpenAt { get; set; } = null!;
    //    public string CloseAt { get; set; } = null!;
    //}
}
