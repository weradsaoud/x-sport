using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos
{
    public class SportMemberShipDto
    {
        public long SportId { get; set; }
        public string SportName { get; set; } = null!;
        public long AcademyId { get; set; }
        public string AcademyName { get; set; } = null!;//TODO there may are more than one academy
        public int UserPoints { get; set; }
    }
}
