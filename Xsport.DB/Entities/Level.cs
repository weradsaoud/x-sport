using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class Level
    {
        public long LevelId { get; set; }
        public int MaxPoints { get; set; }

        //forign keys
        public long SportId { get; set; }
        //navigational properties
        public Sport Sport { get; set; } = null!;
        public ICollection<LevelTranslation> LevelTranslations { get; set; } = null!;
    }
}
