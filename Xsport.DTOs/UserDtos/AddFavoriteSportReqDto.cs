using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.UserDtos
{
    public class AddFavoriteSportReqDto
    {
        public List<long> SportsIds { get; set; } = null!;
    }
}
