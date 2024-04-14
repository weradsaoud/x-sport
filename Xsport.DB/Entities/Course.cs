using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class Course
    {
        public long CourseId { get; set; }
        public decimal Price { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // foriegn keys
        public long AcademyId { get; set; }
        public long SportId { get; set; }
        public long AgeCategoryId { get; set; }
        public long GenderId { get; set; }

        //navigational properties
        public Academy Academy { get; set; } = null!;
        public Sport Sport { get; set; } = null!;
        public AgeCategory AgeCategory { get; set; } = null!;
        public Gender Gender { get; set; } = null!;
        public ICollection<UserCourse> UserCourses { get; set; } = null!;
        public ICollection<CourseWorkingDay> CourseWorkingDays { get; set; } = null!;
        public ICollection<CourseTranslation> CourseTranslations { get; set; } = null!;
    }
}
