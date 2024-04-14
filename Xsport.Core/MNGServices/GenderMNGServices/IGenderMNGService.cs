using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.GenderDtos.MNGDtos;

namespace Xsport.Core.MNGServices.GenderMNGServices
{
    public interface IGenderMNGService
    {
        public Task<long> CreateGender(GenderDto dto);
        public Task<List<GenderDto>> GetGenders(short currentLanguageId);
    }
}
