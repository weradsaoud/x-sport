using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class CourseTranslation
    {
        public long CourseTranslationId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long CourseId { get; set; }
        public long LanguageId { get; set; }

        public Course Course { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
