using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class StadiumService
    {
        public long StadiumServiceId { get; set; }

        //foriegn keys
        public long StadiumId { get; set; }
        public long ServiceId { get; set; }

        //navigational props
        public Stadium Stadium { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}
