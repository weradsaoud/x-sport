using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class ServiceValue
    {
        public long ServiceValueId { get; set; }

        //foriegn keys
        public long ServiceId { get; set; }

        //navigational props
        public Service Service { get; set; } = null!;
        public ICollection<ServiceValueTranslation> ServiceValueTranslations { get; set; } = null!;
        public ICollection<AcademyServiceValue> AcademyServiceValues { get; set; } = null!;
    }
}
