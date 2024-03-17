using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class Service
    {
        public long ServiceId { get; set; }

        //navigational props
        public ICollection<ServiceTranslation> ServiceTranslations { get; set; } = null!;
        public ICollection<ServiceValue> ServiceValues { get; set; } = null!;
    }
}
