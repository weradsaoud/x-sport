using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos
{
    public class AcademyCoursesDto
    {
        public long AcademyId { get; set; }
        public string AcademyName { get; set; } = null!;
        public string CoverPhoto { get; set; } = null!;
        public string CoverVideo { get; set; } = null!;
        public IEnumerable<string> Photos { get; set; } = null!;
        public IEnumerable<string> Videos { get; set; } = null!;
        public string Date { get; set; } = null!;
        public IEnumerable<AgeCategoriesWithCoursesInDate> AgeCategoriesWithCoursesInDate { get; set; } = null!;
    }
    public class AgeCategoriesWithCoursesInDate
    {
        public long AgeCategoryId { get; set; }
        public string AgeCategoryName { get; set; } = null!;
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public IEnumerable<CoursesDto> Courses { get; set; } = null!;
    }
    public class CoursesDto
    {
        public long CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public long SportId { get; set; }
        public string SportName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public String StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;
    }
}
