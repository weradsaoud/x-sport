using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB.Entities;
using Xsport.DTOs.AcademyDtos;
using Xsport.DTOs.AdminDtos;
using Xsport.DTOs.CommonDtos;

namespace Xsport.Core.AcademyServices
{
    public interface IAcademyServices
    {
        public Task<List<UserSportsAcademies>> GetSportsMemberShip(long uId, short currentLanguageId);
        public Task<List<SuggestedAcademyDto>> GetSuggestedAcademies(XsportUser user, PagingDto dto, short currentLanguageId);
        public Task<AboutAcademyDto> GetAboutAcademy(long academyId, short currentLanguageId);
        public Task<AcademyCoursesDto> GetAcademyCoursesToday(
            long academyId, short currentLanguage);
        public Task<AcademyCoursesDto> GetAcademyCoursesInDate(
            long academyId, short currentLanguage, string targetDate);
        public Task<AcademyReviewDto> GetAcademyReviews(long academyId, short currentLanguageId);
        public Task<bool> AddWorkingDays([FromBody] AddWorkingDaysDto dto);
    }
}
