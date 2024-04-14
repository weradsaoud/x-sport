using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class AcademyService
    {
        public long AcademyServiceId { get; set; }


        //foriegn Keys
        public long AcademyId { get; set; }
        public long ServiceId { get; set; }

        //navigational props
        public Academy Academy { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}
