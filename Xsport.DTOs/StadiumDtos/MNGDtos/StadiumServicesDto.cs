using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.StadiumDtos.MNGDtos
{
    public class StadiumServicesDto
    {
        public long StadiumId { get; set; }
        public List<long> ServicesIds { get; set; } = null!;
    }
}
