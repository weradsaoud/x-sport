using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class StadiumTranslation
    {
        public long StadiumTranslationId { get; set; }
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public long StadiumId { get; set; }
        public long LanguageId { get; set; }

        public Stadium Stadium { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
