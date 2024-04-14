using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.CourseDtos.MNGDtos
{
    public class CourseDto
    {
        public string ArName { get; set; } = null!;
        public string EnName { get; set; } = null!;
        public string ArDescription{ get; set; } = null!;
        public string EnArDescription { get; set; } = null!;
        public decimal Price { get; set; }
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public long AcademyId { get; set; }
        public long SportId { get; set; }
        public long AgeCategoryId { get; set; }
        public long GenderId { get; set; }
        public List<CourseWorkingDayDto1> WorkingDays { get; set; } = null!;
    }
    public class CourseWorkingDayDto1
    {
        public long WorkingDayId { get; set; }
        public string StartAt { get; set; } = null!;
        public string EndAt { get; set; } = null!;
    }
}
