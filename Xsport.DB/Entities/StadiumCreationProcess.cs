using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class StadiumCreationProcess
    {
        public long Id { get; set; }
        public required long UserId { get; set; }
        public string CurrentStep { get; set; }
        public long StadiumId { get; set; }
        public Stadium Stadium { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
