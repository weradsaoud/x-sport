using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.ReservationDtos
{
    public class ReservationDetails
    {
        public string SportName { get; set; } = null!;
        public string StadiumName { get; set; } = null!;
        public string StadiumRegion { get; set; } = null!;
        public string ReservationDate { get; set; } = null!;
        public string ReservationTimeFrom { get; set; } = null!;
        public string ReservationTimeTo { get; set; } = null!;
    }
}
