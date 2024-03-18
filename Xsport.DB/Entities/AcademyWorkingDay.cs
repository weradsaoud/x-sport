using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class AcademyWorkingDay
    {
        public long AcademyWorkingDayId { get; set; }
        public TimeOnly OpenAt { get; set; }
        public TimeOnly CloseAt { get; set; }

        //forien keys
        public long AcademyId { get; set; }
        public long WorkingDayId { get; set; }

        //navigational properties
        public Academy Academy { get; set; } = null!;
        public WorkingDay WorkingDay { get; set; } = null!;
    }
}
