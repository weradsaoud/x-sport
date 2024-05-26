using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.StadiumDtos.MNGDtos
{
    public class StadiumFloorDto
    {
        [Required] public int Id { get; set; }
        [Required] public decimal Price { get; set; }
        [Required]
        public long StadiumId { get; set; }
        [Required]
        public long FloorId { get; set; }
    }
}
