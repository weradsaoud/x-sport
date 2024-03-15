using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class WorkingDay
    {
        public long WorkingDayId { get; set; }

        //navigational properties
        public ICollection<WorkingDayTranslation> WorkingDayTranslations { get; set; } = null!;
        public ICollection<AcademyWorkingDay> AcademyWorkingDays { get; set; } = null!;
        public ICollection<CourseWorkingDay> CourseWorkingDays { get; set; } = null!;
    }
}
