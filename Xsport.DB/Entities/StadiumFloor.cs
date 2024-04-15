using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class StadiumFloor
    {
        public long StadiumFloorId { get; set; }

        //foriegn keys
        public long StadiumId { get; set; }
        public long FloorId { get; set; }

        //navigational props
        public Stadium Stadium { get; set; } = null!;
        public Floor Floor { get; set; } = null!;
        public ICollection<Reservation> Reservations { get; set; } = null!;
    }
}
