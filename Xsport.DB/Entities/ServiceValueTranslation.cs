using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class ServiceValueTranslation
    {
        public long ServiceValueTranslationId { get; set; }
        public string Name { get; set; } = null!;
        public long ServiceValueId { get; set; }
        public long LanguageId { get; set; }

        public ServiceValue ServiceValue { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
