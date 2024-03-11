using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.UserDtos
{
    public class EditUserReqDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        [Required]
        public decimal Longitude { get; set; } = 0;
        [Required]
        public decimal Latitude { get; set; } = 0;
        [Required]
        public List<long> SportsIds { get; set; } = null!;
        [Required]
        public string Gender { get; set; } = null!;
        public IFormFile? File { get; set; } = null!;
    }
}
