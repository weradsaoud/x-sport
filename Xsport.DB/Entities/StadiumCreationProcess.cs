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
        public required string UserId { get; set; }
        public int CurrentStep { get; set; }
        public Stadium StadiumData { get; set; } = new Stadium();
        public DateTime LastUpdated { get; set; }
    }
}
