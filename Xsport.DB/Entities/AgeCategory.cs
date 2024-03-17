using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class AgeCategory
    {
        public long AgeCategoryId { get; set; }
        public int FromAge { get; set; }
        public int ToAge { get; set; }

        //foriegn keys
        public long AcademyId { get; set; }

        //navigational properties
        public Academy Academy { get; set; } = null!;
        public ICollection<AgeCategoryTranslation> AgeCategoryTranslations { get; set; } = null!;
        public ICollection<Course> Courses { get; set; } = null!;
    }
}
