using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class AgeCategoryTranslation
    {
        public long AgeCategoryTranslationId { get; set; }
        public string Name { get; set; } = null!;
        public long AgeCategoryId { get; set; }
        public long LanguageId { get; set; }

        public AgeCategory AgeCategory { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
