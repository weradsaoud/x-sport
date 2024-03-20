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
        public string ImgPath { get; set; } = null!;

        //navigational props
        public ICollection<ServiceTranslation> ServiceTranslations { get; set; } = null!;
        public ICollection<StadiumService> StadiumServices { get; set; } = null!;
        public ICollection<AcademyService> AcademyServices { get; set; } = null!;
    }
}
