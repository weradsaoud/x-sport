using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.StadiumDtos.MNGDtos
{
    public class StadiumMultimediaDto
    {
        public long StadiumId { get; set; }
        public IFormFile? CoverPhoto { get; set; } = null!;
        public IFormFile? CoverVideo { get; set; } = null!;
        public List<IFormFile>? Photos { get; set; } = null!;
        public List<IFormFile>? Videos { get; set; } = null!;
    }
}
