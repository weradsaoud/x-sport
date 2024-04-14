using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class UserCourse
    {
        public long UserCourseId { get; set; }
        public bool IsPersonal { get; set; }
        public string Name { get; set; } = null!;
        public string ResidencePlace { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int Points { get; set; }

        //foriegn keys
        public long XsportUserId { get; set; }
        public long CourseId { get; set; }
        public long? RelativeId { get; set; }

        //navigational properties
        public XsportUser XsportUser { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public Relative? Relative { get; set; } = null!;
    }
}
