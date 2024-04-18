using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.DB;
using Xsport.DTOs.ArchiveDtos;
using Xsport.DB.QueryObjects;
using Microsoft.EntityFrameworkCore;
using Xsport.DTOs.AcademyDtos;
using Xsport.Common.Constants;

namespace Xsport.Core.ArchiveServices
{
    public class ArchiveServices : IArchiveServices
    {
        private IRepositoryManager _repositoryManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ArchiveServices(
            IHttpContextAccessor _httpContextAccessor,
            IRepositoryManager repositoryManager)
        {
            httpContextAccessor = _httpContextAccessor;
            _repositoryManager = repositoryManager;
        }

        public async Task<List<AcademyArchiveItem>> AcademiesSubscriptionArchive(
            long uId, short currentLanguageId, AcademyArchiveFilter filter)
        {
            try
            {
                string domainName = httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
                IQueryable<SubscribedAcademyWithDatesDto> SubscribedAcademiesQ = _repositoryManager.UserCourseRepository
                    .FindByCondition(uc => uc.XsportUserId == uId, false)
                    .MapCoursesToMemberShipsDto(currentLanguageId, domainName)
                    .OrderSubscribedAcademies(SubscribedAcademiesOrderOptions.ByCoursePointsDes);
                if (!string.IsNullOrEmpty(filter.FilterByAcademyName))
                    SubscribedAcademiesQ = SubscribedAcademiesQ.FilterSubscribedAcademies(
                        SubscribedAcademiesFilterOptions.FilterByAcademyName, filter.FilterByAcademyName);
                if (filter.FilterByActive.HasValue)
                    SubscribedAcademiesQ = SubscribedAcademiesQ.FilterSubscribedAcademies(
                        SubscribedAcademiesFilterOptions.ByActive, filter.FilterByActive.Value ? "Active" : "InActive");
                if (filter.FilterByLastYear)
                    SubscribedAcademiesQ = SubscribedAcademiesQ.FilterSubscribedAcademies(
                        SubscribedAcademiesFilterOptions.FilterBySubscriptionStartDate,
                        DateOnly.FromDateTime(DateTime.Now.AddYears(-1)).ToString(XsportConstants.DateOnlyFormat));
                if (filter.FilterByLastMonth)
                    SubscribedAcademiesQ = SubscribedAcademiesQ.FilterSubscribedAcademies(
                        SubscribedAcademiesFilterOptions.FilterBySubscriptionStartDate,
                        DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)).ToString(XsportConstants.DateOnlyFormat));
                if (filter.FilterByLastWeek)
                    SubscribedAcademiesQ = SubscribedAcademiesQ.FilterSubscribedAcademies(
                        SubscribedAcademiesFilterOptions.FilterBySubscriptionStartDate,
                        DateOnly.FromDateTime(DateTime.Now.AddDays(-7)).ToString(XsportConstants.DateOnlyFormat));
                IQueryable<SubscribedAcademyDto> SubscribedAcademiesQQ = SubscribedAcademiesQ.MapDatesToStrings();
                List<SubscribedAcademyDto> SubscribedAcademies = await SubscribedAcademiesQQ.ToListAsync();
                return SubscribedAcademies.GroupBy(s => s.AcademyId).Select(g => new AcademyArchiveItem()
                {
                    AcademyId = g.Key,
                    AcademyName = g.First().AcademyName,
                    Sports = g.Select(s => s.Sport).ToList(),
                    SubscriptionStartDate = g.Min(s => DateOnly.Parse(s.CourseStartDate)).ToString(XsportConstants.DateOnlyFormat),
                    SubscriptionEndDate = (g.Max(s => DateOnly.Parse(s.CourseStartDate)) > DateOnly.FromDateTime(DateTime.UtcNow)) ?
                        g.Max(s => DateOnly.Parse(s.CourseStartDate)).ToString(XsportConstants.DateOnlyFormat) :
                        currentLanguageId == (short)LanguagesEnum.English ? "Until Now" : "حتى الآن",
                    Courses = g.Select(s => new AcademyCourseArchiveItem()
                    {
                        CourseId = s.CourseId,
                        CourseName = s.CourseName,
                        CourseStartDate = s.CourseStartDate,
                        CourseEndDate = s.CourseEndDate,
                        KinShip = s.KinShip,
                        SubscriberPoints = s.SubscriberPoints,
                    }).ToList(),
                    CoverPhoto = g.First().CoverPhoto,
                    Photos = g.First().Photos,
                    CoverVideo = g.First().CoverVideo,
                    Videos = g.First().Videos
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
