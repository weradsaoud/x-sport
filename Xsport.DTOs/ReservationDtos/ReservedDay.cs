using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.ReservationDtos
{
    public class ReservedDay
    {
        public string Day { get; set; } = null!;
        public string Date { get; set; } = null!;
        public bool IsWholeDayReserved { get; set; }
        public List<string> ReservedHours { get; set; } = null!;
    }
}
