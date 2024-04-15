using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.ReservationDtos
{
    public class Criteria
    {
        [Required]
        public long SportId { get; set; }
        public string? StadiumName { get; set; }
        public decimal? Long { get; set; }
        public decimal? Lat { get; set; }
        [Required]
        public int PageNum { get; set; }
        [Required]
        public int PageSize { get; set; }
    }
}
