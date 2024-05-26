using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.StadiumDtos.DashboardDtos
{
    public class StadiumStepDataModel
    {
        public long ProcessId { get; set; }
        public StadiumCreationDto? StadiumData { get; set; }
    }
    public class StadiumProcessCreationDto
    {
        public long ProcessId { get; set; }
        public string CurrentStep { get; set; }
        public StadiumDashboardDto? DashboardData { get; set; }
    }
}
