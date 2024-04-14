using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class Gender
    {
        public long GenderId { get; set; }

        //navigational props
        public ICollection<Course> Courses { get; set; } = null!;
        public ICollection<GenderTranslation> GenderTranslations { get; set; } = null!;
    }
}
