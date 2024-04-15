using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class Reservation
    {
        public long ReservationId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly From { get; set; }
        public TimeOnly To { get; set; }
        public short Status { get; set; }

        //foriegn keys
        public long XsportUserId { get; set; }
        public long StadiumFloorId { get; set; }

        //navigational props
        public XsportUser User { get; set; } = null!;
        public StadiumFloor StadiumFloor { get; set; } = null!;
    }
}
