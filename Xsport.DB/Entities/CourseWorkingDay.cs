using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class CourseWorkingDay
    {
        public long CourseWorkingDayId { get; set; }
        public TimeOnly StartAt { get; set; }
        public TimeOnly EndAt { get; set; }

        //forien keys
        public long CourseId { get; set; }
        public long WorkingDayId { get; set; }

        //navigational properties
        public Course Course { get; set; } = null!;
        public WorkingDay WorkingDay { get; set; } = null!;
    }
}
