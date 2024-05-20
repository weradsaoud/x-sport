using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.StadiumDtos.MNGDtos;

namespace Xsport.DTOs.StadiumDtos.DashboardDtos
{
    public class StadiumStepOneDto
    {
        public StadiumDto? GeneralInfo { get; set; }
        public List<StadiumFloorDto>? Floors { get; set; }
        public List<StadiumServicesDto>? Services { get; set; }
        public string? Step { get; set; }
    }
}
