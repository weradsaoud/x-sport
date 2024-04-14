using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.StadiumDtos;

namespace Xsport.Core.StadiumServices
{
    public interface IStadiumServices
    {
        public Task<List<SuggestedStadiumDto>> GetFriendsStadiums(
            long uId, long sportId, int pageNum, int pageSize, short currentLanguageId);
        public Task<List<SuggestedStadiumDto>> GetNearByStadiums(
            long uId, long sportId, int pageNum, int pageSize, short currentLanguageId);
        public Task<AboutStadiumDto> GetAboutStadium(long stadiumId, short currentLanguageId);
    }
}
