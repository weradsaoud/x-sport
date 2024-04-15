using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class Floor
    {
        public long FloorId { get; set; }
        public int NumPlayers { get; set; }

        //foriegn keys
        public long SportId { get; set; }

        //navigational props
        public ICollection<FloorTranslation> FloorTranslations { get; set; } = null!;
        public ICollection<StadiumFloor> StadiumFloors { get; set; } = null!;
        public Sport Sport { get; set; } = null!;
    }
}
