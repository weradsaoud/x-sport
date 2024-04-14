using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.ServiceDtos.MNGDtos
{
    public class PostServiceDto
    {
        public long? Id { get; set; }
        public string ArName { get; set; } = null!;
        public string EnName { get; set; } = null!;
        public IFormFile? Img { get; set; } = null!;
    }
    public class GetServiceDto
    {
        public long? Id { get; set; }
        public string ArName { get; set; } = null!;
        public string EnName { get; set; } = null!;
        public string ImgPath { get; set; } = null!;
    }
}
