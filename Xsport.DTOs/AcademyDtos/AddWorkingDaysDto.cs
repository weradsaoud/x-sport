using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos
{
    public class AddWorkingDaysDto
    {
        public List<AddWorkingDayDto> WorkingDays { get; set; } = null!;
    }

    public class AddWorkingDayDto
    {
        public string ArName { get; set; } = null!;
        public string EnName { get; set; } = null!;
        public int OrderInWeek { get; set; }
    }
}
