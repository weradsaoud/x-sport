using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB.Entities;

namespace Xsport.DTOs.StadiumDtos.DashboardDtos
{
    public class StadiumStepDataModel
    {
        public long ProcessId { get; set; }
        public string UserId { get; set; }
        public int CurrentStep { get; set; }
        public StadiumCreationDto StadiumData { get; set; }
    }
}
