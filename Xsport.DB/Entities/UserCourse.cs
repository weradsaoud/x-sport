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
        public int Points { get; set; }

        //foriegn keys
        public long XsportUserId { get; set; }
        public long CourseId { get; set; }

        //navigational properties
        public XsportUser XsportUser { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
