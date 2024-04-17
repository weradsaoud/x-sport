using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.ArchiveDtos
{
    public class AcademyArchiveItem
    {
        public long AcademyId { get; set; }
        public string AcademyName { get; set; } = null!;
        public string SubscriptionStartDate { get; set; } = null!;
        public string SubscriptionEndDate { get; set; } = null!;
        public List<string> Sports { get; set; } = null!;
        public List<AcademyCourseArchiveItem> Courses { get; set; } = null!;
        public string CoverPhoto { get; set; } = null!;
        public string CoverVideo { get; set; } = null!;
        public List<string> Photos { get; set; } = null!;
        public List<string> Videos { get; set; } = null!;
    }
    public class AcademyCourseArchiveItem
    {
        public long CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string CourseStartDate { get; set; } = null!;
        public string CourseEndDate { get; set; } = null!;
        public string KinShip { get; set; } = null!;
        public int SubscriberPoints { get; set; }
    }
}
