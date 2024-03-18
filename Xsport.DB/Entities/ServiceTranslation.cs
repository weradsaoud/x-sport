using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class ServiceTranslation
    {
        public long ServiceTranslationId { get; set; }
        public string Name { get; set; } = null!;
        public long ServiceId { get; set; }
        public long LanguageId { get; set; }

        public Service Service{ get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
