using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AdminDtos
{
    public class AddCourseDto
    {
        public string ArName { get; set; } = null!;
        public string EnName { get; set; } = null!;
        public string ArDescription{ get; set; } = null!;
        public string EnDescription{ get; set; } = null!;
        public decimal Price { get; set; }
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public List<CourseWorkingDayDto> WorkingDays { get; set; } = null!;
        // foriegn keys
        public long AcademyId { get; set; }
        public long SportId { get; set; }
        public long AgeCategoryId { get; set; }
    }
    public class CourseWorkingDayDto
    {
        public long WorkingDayId { get; set; }
        public string StartAt { get; set; } = null!;
        public string EndAt { get; set; } = null!;
    }
}
