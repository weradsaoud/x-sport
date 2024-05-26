using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class Mutimedia
    {
        public long MutimediaId { get; set; }
        public string? FilePath { get; set; } = null!;
        public bool IsVideo { get; set; }
        public bool IsCover { get; set; }

        //foriegn keys
        public long? AcademyId { get; set; }
        public long? StadiumId { get; set; }

        // navigational properties
        public Academy? Academy { get; set; } = null!;
        public Stadium? Stadium { get; set; } = null!;
    }
}
