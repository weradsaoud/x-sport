using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class LevelTranslation
    {
        public long LevelTranslationId { get; set; }
        public string? Name { get; set; }
        [Required]
        public long LevelId { get; set; }
        [Required]
        public long LanguageId { get; set; }

        public Level Level { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
