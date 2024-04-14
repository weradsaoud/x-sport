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

        //navigational props
        public ICollection<FloorTranslation> FloorTranslations { get; set; } = null!;
        public ICollection<Stadium> Stadiums { get; set; } = null!;
    }
}
