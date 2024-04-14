using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.CourseDtos.MNGDtos;
using Xsport.DTOs.RelativeDtos.MNGDtos;

namespace Xsport.Core.MNGServices.RelativeMNGServices
{
    public interface IRelativeMNGService
    {
        public Task<long> CreateRelative(RelativeDto dto);
        public Task<List<RelativeDto>> GetRelatives();
    }
}
