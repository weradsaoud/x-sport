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
        public Task<bool> AddAcademy(AddAcademyDto dto);
        public Task<bool> AddService(AddServiceDto dto);
        public Task<bool> AddWorkingDays([FromBody] AddWorkingDaysDto dto);
        public Task<List<GetWorkingDayDto>> GetWorkingDays(short currentLanguageId);
        public Task<List<GetServicesDto>> GetServices(short currentLanguageId);
        public Task<AgeCategory> AddAgeCategory(AddAgeCategoryDto dto);
        public Task<Course> AddCourse(AddCourseDto dto);
        public Task<bool> InrollUserInCourse(long uId, long courseId, bool isPersonal);
        public Task<bool> AddServiceToAcademy(long academyId, long serviceValueId);
    }
}
