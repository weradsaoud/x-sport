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
        public static IQueryable<SubscribedAcademyDto> MapDatesToStrings(this IQueryable<SubscribedAcademyWithDatesDto> subscribedAcademies)
        {
            try
            {
                return subscribedAcademies.Select(sa => new SubscribedAcademyDto()
                {
                    AcademyId = sa.AcademyId,
                    AcademyName = sa.AcademyName,
                    CourseId = sa.CourseId,
                    CourseName = sa.CourseName,
                    CourseStartDate = sa.CourseStartDate.ToString(XsportConstants.DateOnlyFormat),
                    CourseEndDate = sa.CourseEndDate.ToString(XsportConstants.DateOnlyFormat),
                    KinShip = sa.KinShip,
                    SubscriberPoints = sa.SubscriberPoints,
                    Sport = sa.Sport,
                    CoverPhoto = sa.CoverPhoto,
                    CoverVideo = sa.CoverVideo,
                    Photos = sa.Photos,
                    Videos = sa.Videos,
                });
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static IQueryable<SubscribedAcademyWithDatesDto> MapCoursesToMemberShipsDto(
            this IQueryable<UserCourse> userCourses, short currentLanguageId, string domainName)
        {
            try
            {
                return userCourses
                    .Select(uc => new SubscribedAcademyWithDatesDto()
                    {
                        AcademyId = uc.Course.AcademyId,
                        AcademyName = uc.Course.Academy.AcademyTranslations
                        .Single(t => t.LanguageId == currentLanguageId).Name,
                        CourseId = uc.Course.CourseId,
                        CourseName = uc.Course.CourseTranslations
                        .Single(t => t.LanguageId == currentLanguageId).Name,
                        CourseStartDate = uc.Course.StartDate,
                        CourseEndDate = uc.Course.EndDate,
                        KinShip = uc.IsPersonal ?
                        (currentLanguageId == (long)LanguagesEnum.English ? "You" : "أنت") :
                        uc.Relative.RelativeTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                        SubscriberPoints = uc.Points,
                        Sport = uc.Course.Sport.SportTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
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

        public static IQueryable<SubscribedAcademyWithDatesDto> OrderSubscribedAcademies(
            this IQueryable<SubscribedAcademyWithDatesDto> subscribedAcademies,
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
        public static IQueryable<SubscribedAcademyWithDatesDto> FilterSubscribedAcademies(
            this IQueryable<SubscribedAcademyWithDatesDto> subscribedAcademies,
            SubscribedAcademiesFilterOptions option, string? value)
        {
            //if (string.IsNullOrEmpty(value)) return subscribedAcademies;
            switch (option)
            {
                case SubscribedAcademiesFilterOptions.None:
                    return subscribedAcademies;
                case SubscribedAcademiesFilterOptions.ByActive:
                    if (!string.IsNullOrEmpty(value))
                    {
                        DateOnly todyDate = DateOnly.FromDateTime(DateTime.Today);
                        if (value == "Active")
                            return subscribedAcademies
                                .Where(
                                sa => sa.CourseStartDate <= todyDate &&
                                sa.CourseEndDate >= todyDate);
                        if (value == "InActive")
                            return subscribedAcademies
                                .Where(
                                sa => sa.CourseStartDate >= todyDate ||
                                sa.CourseEndDate <= todyDate);
                        return subscribedAcademies;
                    }
                    else
                        return subscribedAcademies;
                case SubscribedAcademiesFilterOptions.FilterByAcademyName:
                    if (!string.IsNullOrEmpty(value))
                        return subscribedAcademies
                            .Where(sa => sa.AcademyName.ToLower().Contains(value.ToLower()));
                    else
                        return subscribedAcademies;
                case SubscribedAcademiesFilterOptions.FilterBySubscriptionStartDate:
                    if (!string.IsNullOrEmpty(value))
                    {
                        DateOnly subscriptionStartDate;
                        try
                        {
                            subscriptionStartDate = DateOnly.Parse(value);
                        }
                        catch(Exception ex)
                        {
                            throw new Exception("Please, Provide a valid Date.");
                        }
                        return subscribedAcademies
                            .Where(sa => sa.CourseStartDate >= subscriptionStartDate);
                    }
                    else
                        return subscribedAcademies;
                case SubscribedAcademiesFilterOptions.FilterBySubscriptionEndtDate:
                    if (!string.IsNullOrEmpty(value))
                    {
                        DateOnly subscriptionEndDate;
                        try
                        {
                            subscriptionEndDate = DateOnly.Parse(value);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Please, Provide a valid Date.");
                        }
                        return subscribedAcademies
                            .Where(sa => sa.CourseEndDate >= subscriptionEndDate);
                    }
                    else
                        return subscribedAcademies;
                default:
                    throw new ArgumentOutOfRangeException(
                    nameof(option), option, null);
            }
        }
    }
}
