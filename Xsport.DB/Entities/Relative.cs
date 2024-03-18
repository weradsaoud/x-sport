using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class Relative
    {
        public long RelativeId { get; set; }

        //navigational properties
        public ICollection<RelativeTranslation> RelativeTranslations { get; set; } = null!;
        public ICollection<UserCourse> UserCourses { get; set; } = null!;
    }
}
