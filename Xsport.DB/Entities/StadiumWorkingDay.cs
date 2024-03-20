using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class StadiumWorkingDay
    {
        public long StadiumWorkingDayId { get; set; }
        public TimeOnly OpenAt { get; set; }
        public TimeOnly CloseAt { get; set; }

        //foriegn Keys
        public long StadiumId { get; set; }
        public long WorkingDayId { get; set; }

        //navigational props
        public Stadium Stadium { get; set; } = null!;
        public WorkingDay WorkingDay { get; set; } = null!;
    }
}
