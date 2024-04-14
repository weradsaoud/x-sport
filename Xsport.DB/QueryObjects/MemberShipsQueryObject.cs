using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Constants;
using Xsport.Common.Enums;
using Xsport.DB.Entities;
using Xsport.DTOs.AcademyDtos;

namespace Xsport.DB.QueryObjects
{
    public static class MemberShipsQueryObject
    {
        public static IQueryable<SubscribedAcademyDto> MapCoursesToMemberShipsDto(
            this IQueryable<UserCourse> userCourses, short currentLanguageId, string domainName)
        {
            try
            {
                return userCourses
                    .Where(
                    uc => uc.Course.EndDate >= DateOnly.FromDateTime(DateTime.Today))
                    .Select(uc => new SubscribedAcademyDto()
                    {
                        AcademyId = uc.Course.AcademyId,
                        AcademyName = uc.Course.Academy.AcademyTranslations
                    .Single(t => t.LanguageId == currentLanguageId).Name,
                        CourseId = uc.Course.CourseId,
                        CourseName = uc.Course.CourseTranslations
                    .Single(t => t.LanguageId == currentLanguageId).Name,
                        CourseStartDate = uc.Course.StartDate.ToString(XsportConstants.DateOnlyFormat),
                        CourseEndDate = uc.Course.EndDate.ToString(XsportConstants.DateOnlyFormat),
                        KinShip = uc.IsPersonal ?
                    (currentLanguageId == (long)LanguagesEnum.English ? "You" : "أنت") :
                    uc.Relative.RelativeTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                        SubscriberPoints = uc.Points,
                        CoverPhoto = string.IsNullOrEmpty(
                        uc.Course.Academy.Mutimedias.Single(m => m.IsCover && !m.IsVideo).FilePath) ? ""
                    : domainName + "/Images/" + uc.Course.Academy.Mutimedias.Single(m => m.IsCover && !m.IsVideo).FilePath,
                        CoverVideo = string.IsNullOrEmpty(
                        uc.Course.Academy.Mutimedias.Single(m => m.IsCover && m.IsVideo).FilePath) ? ""
                    : domainName + "/Images/" + uc.Course.Academy.Mutimedias.Single(m => m.IsCover && m.IsVideo).FilePath,
                        Photos = uc.Course.Academy.Mutimedias
                    .Where(m => !m.IsVideo && !m.IsCover)
                    .Select(m => string.IsNullOrEmpty(m.FilePath) ? "" : domainName + "/Images/" + m.FilePath).ToList(),
                        Videos = uc.Course.Academy.Mutimedias
                    .Where(m => m.IsVideo && !m.IsCover)
                    .Select(m => string.IsNullOrEmpty(m.FilePath) ? "" : domainName + "/Images/" + m.FilePath).ToList(),
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static IQueryable<SubscribedAcademyDto> OrderSubscribedAcademies(
            this IQueryable<SubscribedAcademyDto> subscribedAcademies,
            SubscribedAcademiesOrderOptions option)
        {
            switch (option)
            {
                case SubscribedAcademiesOrderOptions.None:
                    return subscribedAcademies;
                case SubscribedAcademiesOrderOptions.SimpleOrder:
                    return subscribedAcademies.OrderBy(sa => sa.AcademyId);
                case SubscribedAcademiesOrderOptions.ByCoursePointsDes:
                    return subscribedAcademies.OrderByDescending(sa => sa.SubscriberPoints);
                case SubscribedAcademiesOrderOptions.ByCoursePointsAsen:
                    return subscribedAcademies.OrderBy(sa => sa.SubscriberPoints);
                default:
                    throw new ArgumentOutOfRangeException(
                    nameof(option), option, null);
            }
        }
        public static IQueryable<SubscribedAcademyDto> FilterSubscribedAcademies(
            this IQueryable<SubscribedAcademyDto> subscribedAcademies,
            SubscribedAcademiesFilterOptions option/*, string value*/)
        {
            //if (string.IsNullOrEmpty(value)) return subscribedAcademies;
            switch (option)
            {
                case SubscribedAcademiesFilterOptions.None:
                    return subscribedAcademies;
                case SubscribedAcademiesFilterOptions.Active:
                    return subscribedAcademies
                        .Where(
                        sa => DateOnly.Parse(sa.CourseStartDate) <= DateOnly.FromDateTime(DateTime.Today) &&
                        DateOnly.Parse(sa.CourseEndDate) >= DateOnly.FromDateTime(DateTime.Today));
                case SubscribedAcademiesFilterOptions.InActive:
                    return subscribedAcademies
                        .Where(
                        sa => DateOnly.Parse(sa.CourseStartDate) >= DateOnly.FromDateTime(DateTime.Today) &&
                        DateOnly.Parse(sa.CourseEndDate) <= DateOnly.FromDateTime(DateTime.Today));
                default:
                    throw new ArgumentOutOfRangeException(
                    nameof(option), option, null);
            }
        }
    }
}
