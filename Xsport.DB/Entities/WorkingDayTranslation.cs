using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class WorkingDayTranslation
    {
        public long WorkingDayTranslationId { get; set; }
        public string Name { get; set; } = null!;
        public long WorkingDayId { get; set; }
        public long LanguageId { get; set; }

        public WorkingDay WorkingDay { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
