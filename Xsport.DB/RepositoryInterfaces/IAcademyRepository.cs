using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.DB.Entities;
using Xsport.DTOs.AcademyDtos;
using Xsport.DTOs.AdminDtos;

namespace Xsport.DB.RepositoryInterfaces
{
    public interface IAcademyRepository : IRepositoryBase<Academy>
    {
        public List<SportMemberShipDto> GetUserSportsAcademies(
            long uId, short currentLanguageId);
        public IQueryable<SuggestedAcademyDto> GetSuggestedAcademies(
            short currentLanguageId,
            SuggestedAcademiesOrderOptions orderOption,
            SuggestedAcademiesFilterOptions filterOption, string filterValue,
            int pageNumber, int pageSize);
        public Task<AboutAcademyDto> GetAboutAcademy(
            long academyId, short currentLanguageId);
        public Task<AcademyCoursesDto> GetCoursesInDate(
            long academyId, short currentLanguageId, DateOnly targetDate);
        public Task<AcademyReviewDto> GetAcademyReviews(long academyId, short currentLanguageId);
        public Task<Academy> AddAcademy(
            AddAcademyDto dto, string coverPhotoPath, string coverVideoPath,
            List<string> photosPathes, List<string> vidoessPathes);
        public Task<bool> AddService(AddServiceDto dto);
        public Task<bool> AddWorkingDays(AddWorkingDaysDto dto);
        public Task<List<GetServicesDto>> GetServices(short currentLanguageId);
        public Task<List<GetWorkingDayDto>> GetWorkingDays(short currentLanguageId);
        public Task<AgeCategory> AddAgeCategory(AddAgeCategoryDto dto);
        public Task<Course> AddCourse(AddCourseDto dto);
        public Task<bool> InrollUserInCourse(long uId, long courseId, bool isPersonal);
        public Task<bool> AddServiceToAcademy(long academyId, long serviceValueId);

    }
}
