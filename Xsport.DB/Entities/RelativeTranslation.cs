using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class RelativeTranslation
    {
        public long RelativeTranslationId { get; set; }
        public string Name { get; set; } = null!;
        [Required]
        public long RelativeId { get; set; }
        [Required]
        public long LanguageId { get; set; }

        public Relative Relative { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
