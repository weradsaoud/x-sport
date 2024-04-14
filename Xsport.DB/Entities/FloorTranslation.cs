using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class FloorTranslation
    {
        public long FloorTranslationId { get; set; }
        public string Name { get; set; } = null!;
        public long FloorId { get; set; }
        public long LanguageId { get; set; }

        public Floor Floor { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
