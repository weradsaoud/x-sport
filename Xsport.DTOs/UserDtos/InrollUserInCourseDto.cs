using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.UserDtos
{
    public class InrollUserInCourseDto
    {
        public long UId { get; set; }
        public long CourseId { get; set; }
        public bool IsPersonal { get; set; }
    }
}
