using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class Academy
    {
        public long AcademyId { get; set; }
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
        public string Phone { get; set; } = null!;
        //public string Description { get; set; } = null!;
        public TimeOnly OpenAt { get; set; }
        public TimeOnly CloseAt { get; set; }

        //navigational properties
        public ICollection<AcademyTranslation> AcademyTranslations { get; set; } = null!;
        public ICollection<Course> Courses { get; set; } = null!;
        public ICollection<AcademyWorkingDay> AcademyWorkingDays { get; set; } = null!;
        public ICollection<Mutimedia> Mutimedias { get; set; } = null!;
        public ICollection<AcademyReview> AcademyReviews { get; set; } = null!;
        public ICollection<AcademyServiceValue> AcademyServiceValues { get; set; } = null!;
        public ICollection<AgeCategory> AgeCategorys { get; set; } = null!;
    }
}
