using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.UserDtos;

namespace Xsport.Core.SportServices
{
    public interface ISportServices
    {
        public Task<List<SportDto>> GetSports(long CurrentLanguageId);
    }
}
