using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos
{
    public class GetWorkingDayDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public int Order { get; set; }
    }
}
