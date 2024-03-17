using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class AcademyServiceValue
    {
        public long AcademyServiceValueId { get; set; }

        //forgien keys
        public long AcademyId { get; set; }
        public long ServiceValueId { get; set; }

        //navigational props
        public Academy Academy { get; set; } = null!;
        public ServiceValue ServiceValue { get; set; } = null!;
    }
}
