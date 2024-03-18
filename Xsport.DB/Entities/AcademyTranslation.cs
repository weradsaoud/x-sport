using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class AcademyTranslation
    {
        public long AcademyTranslationId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long AcademyId { get; set; }
        public long LanguageId { get; set; }

        public Academy Academy { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
