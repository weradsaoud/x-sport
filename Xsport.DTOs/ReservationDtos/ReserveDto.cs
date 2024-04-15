using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.ReservationDtos
{
    public class ReserveDto
    {
        [Required]
        public long StadiumFloorId { get; set; }
        [Required]
        public string ReservationDate { get; set; } = null!;
        [Required]
        public string ReservatonTimeFrom { get; set; } = null!;
        [Required]
        public string ReservatonTimeTo{ get; set; } = null!;
    }
}
