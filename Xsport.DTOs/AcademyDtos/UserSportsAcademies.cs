using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos
{
    public class UserSportsAcademies
    {
        public SportInfo SportInfo { get; set; } = null!;
        public List<AcademyInfo> AcademyInfoes { get; set; } = null!;
    }
    public class SportInfo
    {
        public long SportId { get; set; }
        public string Name { get; set; } = null!;
    }
    public class AcademyInfo
    {
        public long AcademyId { get; set; }
        public string Name { get; set; } = null!;
        public int Points { get; set; }
    }
}
