using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.GenderDtos.MNGDtos
{
    public class GenderDto
    {
        public long? Id { get; set; }
        [Required]
        public string ArName { get; set; } = null!;
        [Required]
        public string EnName { get; set; } = null!;
    }
}
