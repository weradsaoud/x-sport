using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AgeCategoryDtos.MNGDtos
{
    public class AgeCategoryDto
    {
        public long Id { get; set; }
        public long AcademyId { get; set; }
        public string ArName { get; set; } = null!;
        public string EnName { get; set; } = null!;
        public int FromAge { get; set; }
        public int ToAge { get; set; }
    }
}
