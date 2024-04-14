using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class GenderTranslation
    {
        public long GenderTranslationId { get; set; }
        public string Name { get; set; } = null!;
        public long GenderId { get; set; }
        public long LanguageId { get; set; }

        public Gender Gender { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
