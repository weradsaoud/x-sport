using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos.MNGDtos
{
    public class PostAcademyDto
    {
        public string ArName { get; set; } = null!;
        public string EnName { get; set; } = null!;
        public string ArDescription { get; set; } = null!;
        public string EnDescription { get; set; } = null!;
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
        public string Phone { get; set; } = null!;
        public string OpenAt { get; set; } = null!;
        public string CloseAt { get; set; } = null!;
    }
}
