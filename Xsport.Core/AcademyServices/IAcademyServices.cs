using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB.Entities;
using Xsport.DTOs;
using Xsport.DTOs.AcademyDtos;
using Xsport.DTOs.AdminDtos;
using Xsport.DTOs.AgeCategoryDtos;
using Xsport.DTOs.CommonDtos;
using Xsport.DTOs.GenderDtos;

namespace Xsport.Core.AcademyServices
{
    public interface IAcademyServices
    {
        public Task<List<SubscribedAcademyDto>> GetMemberShips(long uId, short currentLanguageId);
        public Task<SuggestedAcademiesDto> GetSuggestedAcademies(XsportUser user, long sportId, PagingDto dto, short currentLanguageId);
        public Task<SuggestedAcademiesDto> GetAllAcademies(XsportUser user, PagingDto dto, short currentLanguageId);
        public Task<AboutAcademyDto> GetAboutAcademy(long academyId, short currentLanguageId);
        //public Task<AcademyCoursesDto> GetAcademyCoursesToday(
        //    long academyId, short currentLanguage);
        //public Task<AcademyCoursesDto> GetAcademyCoursesInDate(
        //    long academyId, short currentLanguage, string targetDate);
        public Task<AcademyReviewDto> GetAcademyReviews(long academyId, short currentLanguageId);
        public Task<bool> AddWorkingDays(AddWorkingDaysDto dto);
        public Task<List<AgeCategoryWithCoursesDto>> GetAcademyCourses(long academyId, short currentLanguageId);
        public Task<List<DropDownDto>> GetAgeCategories(long academyId, short currentLanguageId);
        public Task<List<DropDownDto>> GetGenders(short currentLanguageId);
        public Task<List<DropDownDto>> GetRelatives(short currentLanguageId);
        public Task<List<AgeCategoryCourseDto>> GetCoursesToSubscribe(
            long academyId, long ageCategoryId, long genderId, short currentLanguageId);
    }
}
