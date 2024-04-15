using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.StadiumDtos.MNGDtos
{
    public class StadiumDto
    {
        public long Id { get; set; }
        public long? AcademyId { get; set; }
        public string ArName { get; set; } = null!;
        public string EnName { get; set; } = null!;
        public string ArDescription { get; set; } = null!;
        public string EnDescription { get; set; } = null!;
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public decimal Price { get; set; }
    }
}
