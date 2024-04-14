using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.RelativeDtos.MNGDtos
{
    public class RelativeDto
    {
        public long? Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string ArName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string EnName { get; set; } = null!;
    }
}
