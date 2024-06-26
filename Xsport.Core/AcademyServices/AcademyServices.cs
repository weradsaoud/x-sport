﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using Xsport.Common.Constants;
using Xsport.Common.Enums;
using Xsport.Common.Utils;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DB.QueryObjects;
using Xsport.DTOs;
using Xsport.DTOs.AcademyDtos;
using Xsport.DTOs.AdminDtos;
using Xsport.DTOs.AgeCategoryDtos;
using Xsport.DTOs.CommonDtos;
using Xsport.DTOs.GenderDtos;

namespace Xsport.Core.AcademyServices
{
    public class AcademyServices : IAcademyServices
    {
        private IRepositoryManager _repositoryManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AcademyServices(
            IRepositoryManager repositoryManager,
            IWebHostEnvironment _webHostEnvironment,
            IHttpContextAccessor _httpContextAccessor)
        {
            _repositoryManager = repositoryManager;
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
        }

        public async Task<List<SubscribedAcademyDto>> GetMemberShips(long uId, short currentLanguageId)
        {
            try
            {
                string domainName = httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
                return await _repositoryManager.UserCourseRepository.FindByCondition(uc => uc.XsportUserId == uId, false)
                    .MapCoursesToMemberShipsDto(currentLanguageId, domainName)
                    .OrderSubscribedAcademies(SubscribedAcademiesOrderOptions.ByCoursePointsDes)
                    .FilterSubscribedAcademies(SubscribedAcademiesFilterOptions.ByActive, "Active")
                    .MapDatesToStrings()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<List<UserSportsAcademies>> GetSportsMemberShip(long uId, short currentLanguageId)
        //{
        //    XsportUser user = await _repositoryManager.XsportUserRepository
        //        .FindByCondition(u => u.Id == uId, false)
        //        .Include(u => u.UserCourses)
        //        .ThenInclude(uc => uc.Course)
        //        .ThenInclude(c => c.Academy)
        //        .ThenInclude(a => a.AcademyTranslations)
        //        .Include(u => u.UserCourses)
        //        .ThenInclude(uc => uc.Course)
        //        .ThenInclude(c => c.Sport)
        //        .ThenInclude(s => s.SportTranslations)
        //        .SingleOrDefaultAsync() ?? throw new Exception("User does not exist.");
        //    var userSportsAcademies = user.UserCourses.Where(uc => uc.IsPersonal)
        //        .Select(uc => new SportMemberShipDto()
        //        {
        //            AcademyId = uc.Course.AcademyId,
        //            AcademyName = uc.Course.Academy.AcademyTranslations
        //                .Single(t => t.LanguageId == currentLanguageId).Name,
        //            SportId = uc.Course.SportId,
        //            SportName = uc.Course.Sport.SportTranslations
        //                .Single(t => t.LanguageId == currentLanguageId).Name,
        //            UserPoints = uc.Points
        //        }).ToList();
        //    var SportsAcademiesGroupedBySportId = userSportsAcademies.GroupBy(x => x.SportId);
        //    List<long> sportsIds = new List<long>();
        //    List<UserSportsAcademies> sportsAcademies = new List<UserSportsAcademies>();
        //    foreach (var group in SportsAcademiesGroupedBySportId)
        //    {
        //        sportsIds.Add(group.Key);
        //        SportInfo sportInfo = new SportInfo();
        //        List<AcademyInfo> academiesInfo = new List<AcademyInfo>();
        //        foreach (var userSportAcademy in group)
        //        {
        //            academiesInfo.Add(new AcademyInfo()
        //            {
        //                AcademyId = userSportAcademy.AcademyId,
        //                Name = userSportAcademy.AcademyName,
        //                Points = userSportAcademy.UserPoints
        //            });
        //            sportInfo = new SportInfo()
        //            {
        //                SportId = userSportAcademy.SportId,
        //                Name = userSportAcademy.SportName
        //            };
        //        }
        //        sportsAcademies.Add(new UserSportsAcademies()
        //        {
        //            SportInfo = sportInfo,
        //            AcademyInfoes = academiesInfo
        //        });
        //    }
        //    var restSports = await _repositoryManager.SportRepository
        //        .FindByCondition(s => !sportsIds.Contains(s.SportId), false)
        //        .Select(s => new UserSportsAcademies()
        //        {
        //            SportInfo = new SportInfo()
        //            {
        //                SportId = s.SportId,
        //                Name = s.SportTranslations
        //                .Single(t => t.LanguageId == currentLanguageId).Name,
        //            },
        //            AcademyInfoes = new List<AcademyInfo>()
        //        }).ToListAsync();
        //    sportsAcademies.AddRange(restSports.OrderBy(r => r.SportInfo.SportId));
        //    return sportsAcademies;
        //}
        public async Task<SuggestedAcademiesDto> GetSuggestedAcademies(
            XsportUser user, long sportId, PagingDto dto, short currentLanguageId)
        {
            try
            {
                string domainName = httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
                if (sportId == 0) throw new Exception("Please, provide a valide sport.");
                Sport sport = await _repositoryManager.SportRepository
                    .FindByCondition(s => s.SportId == sportId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Sport does not exist.");
                var academies = await _repositoryManager.AcademyRepository
                    .FindByCondition(a => a.Courses.Select(c => c.SportId).Contains(sportId), false)
                    .MapAcademiesToSuggested(currentLanguageId, domainName)
                    .OrderSuggestedAcademies(SuggestedAcademiesOrderOptions.EvaluationDown)
                    .FilterSuggestedAcademies(SuggestedAcademiesFilterOptions.None, string.Empty)
                    .Page<SuggestedAcademyDto>(dto.PageNumber, dto.PageSize).ToListAsync();
                //var suggestedAcademies = await academies
                //    .Where(a => Utils.CalculateDistanceBetweenTowUsers(
                //        user.Latitude ?? 0, user.Longitude ?? 0, a.Lat, a.Long)
                //    <= XsportConstants.SameAreaRaduis).ToListAsync();

                SuggestedAcademiesDto suggestedAcademies = new SuggestedAcademiesDto()
                {
                    SuggestedAcademies = academies,
                    GendersDropdownItems = await GetGenders(currentLanguageId),
                    RelativesDropdownItems = await GetRelatives(currentLanguageId),
                };
                return suggestedAcademies;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<SuggestedAcademiesDto> GetAllAcademies(
            XsportUser user, PagingDto dto, short currentLanguageId)
        {
            try
            {
                string domainName = httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
                var academiesQuery = _repositoryManager.AcademyRepository.FindAll(false)
                    .MapAcademiesToSuggested(currentLanguageId, domainName)
                    .OrderSuggestedAcademies(SuggestedAcademiesOrderOptions.EvaluationDown)
                    .FilterSuggestedAcademies(SuggestedAcademiesFilterOptions.None, string.Empty)
                    .Page<SuggestedAcademyDto>(dto.PageNumber, dto.PageSize);
                var academies = await academiesQuery.ToListAsync();
                //var suggestedAcademies = await academies
                //    .Where(a => Utils.CalculateDistanceBetweenTowUsers(
                //        user.Latitude ?? 0, user.Longitude ?? 0, a.Lat, a.Long)
                //    <= XsportConstants.SameAreaRaduis).ToListAsync();
                SuggestedAcademiesDto suggestedAcademies = new SuggestedAcademiesDto()
                {
                    SuggestedAcademies = academies,
                    GendersDropdownItems = await GetGenders(currentLanguageId),
                    RelativesDropdownItems = await GetRelatives(currentLanguageId),
                };
                return suggestedAcademies;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<AboutAcademyDto> GetAboutAcademy(
            long academyId, short currentLanguageId)
        {
            try
            {
                string domainName = httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
                return await _repositoryManager.AcademyRepository
                    .FindByCondition(a => a.AcademyId == academyId, false)
                    .Select(academy => new AboutAcademyDto()
                    {
                        AcademyId = academy.AcademyId,
                        Name = academy.AcademyTranslations
                        .Single(t => t.LanguageId == currentLanguageId).Name,
                        Description = academy.AcademyTranslations
                        .Single(t => t.LanguageId == currentLanguageId).Description,
                        Phone = academy.Phone,
                        Lat = academy.Lattitude,
                        Long = academy.Longitude,
                        OpenAt = academy.AcademyWorkingDays
                        .Single(w => w.WorkingDay.OrderInWeek == (int)DateTime.Today.DayOfWeek)
                        .OpenAt.ToString(XsportConstants.TimeOnlyFormat),
                        CloseAt = academy.AcademyWorkingDays
                        .Single(w => w.WorkingDay.OrderInWeek == (int)DateTime.Today.DayOfWeek)
                        .CloseAt.ToString(XsportConstants.TimeOnlyFormat),
                        MinPrice = academy.Courses.OrderBy(c => c.Price).First().Price,
                        MaxPrice = academy.Courses
                        .OrderByDescending(c => c.Price).First().Price,
                        services = academy.AcademyServices.Select(a => new ServiceDto1()
                        {
                            ServiceId = a.ServiceId,
                            ServiceName = a.Service.ServiceTranslations
                            .Single(t => t.LanguageId == currentLanguageId).Name,
                            ImgUrl = string.IsNullOrEmpty(a.Service.ImgPath) ? ""
                            : domainName + "/Images/" + a.Service.ImgPath
                        })
                    }).SingleAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<AgeCategoryWithCoursesDto>> GetAcademyCourses(long academyId, short currentLanguageId)
        {
            try
            {
                Academy academy = await _repositoryManager.AcademyRepository
                    .FindByCondition(a => a.AcademyId == academyId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Academy does not exist.");
                return await _repositoryManager.AgeCategoryRepository.FindByCondition(a => a.AcademyId == academyId, false)
                     .Select(a => new AgeCategoryWithCoursesDto()
                     {
                         AgeCategoryId = a.AgeCategoryId,
                         AgeCategoryName = a.AgeCategoryTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                         FromAge = a.FromAge,
                         ToAge = a.ToAge,
                         Courses = a.Courses.Select(c => new AgeCategoryCourseDto()
                         {
                             CourseId = c.CourseId,
                             CourseName = c.CourseTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                             Description = c.CourseTranslations.Single(t => t.LanguageId == currentLanguageId).Description,
                             Gender = c.Gender.GenderTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                             SportId = c.SportId,
                             SportName = c.Sport.SportTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                             Price = c.Price,
                             StartDate = c.StartDate.ToString(XsportConstants.DateOnlyFormat),
                             EndDate = c.EndDate.ToString(XsportConstants.DateOnlyFormat)
                         })
                     }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<AgeCategoryCourseDto>> GetCoursesToSubscribe(
            long academyId, long ageCategoryId, long genderId, short currentLanguageId)
        {
            try
            {
                Academy academy = await _repositoryManager.AcademyRepository
                    .FindByCondition(a => a.AcademyId == academyId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Academy does not exist.");
                AgeCategory ageCategory = await _repositoryManager.AgeCategoryRepository
                    .FindByCondition(ac => ac.AgeCategoryId == ageCategoryId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("AgeCategory does not exist.");
                Gender gender = await _repositoryManager.GenderRepository
                    .FindByCondition(g => g.GenderId == genderId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Gender does not exist.");
                return await _repositoryManager.CourseRepository
                    .FindByCondition(c =>
                    c.AcademyId == academyId &&
                    c.AgeCategoryId == ageCategoryId &&
                    c.GenderId == genderId, false).Select(c => new AgeCategoryCourseDto()
                    {
                        CourseId = c.CourseId,
                        CourseName = c.CourseTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                        Description = c.CourseTranslations.Single(t => t.LanguageId == currentLanguageId).Description,
                        Gender = c.Gender.GenderTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                        SportId = c.SportId,
                        SportName = c.Sport.SportTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                        Price = c.Price,
                        StartDate = c.StartDate.ToString(XsportConstants.DateOnlyFormat),
                        EndDate = c.EndDate.ToString(XsportConstants.DateOnlyFormat)
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<DropDownDto>> GetAgeCategories(long academyId, short currentLanguageId)
        {
            try
            {
                Academy academy = await _repositoryManager.AcademyRepository
                    .FindByCondition(a => a.AcademyId == academyId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Academy does not exist.");
                return await _repositoryManager.AgeCategoryRepository
                    .FindByCondition(ac => ac.AcademyId == academyId, false)
                    .Select(ac => new DropDownDto()
                    {
                        Id = ac.AgeCategoryId,
                        Name = ac.AgeCategoryTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<DropDownDto>> GetGenders(short currentLanguageId)
        {
            try
            {
                return await _repositoryManager.GenderRepository.FindAll(false)
                    .Select(g =>
                    new DropDownDto
                    {
                        Id = g.GenderId,
                        Name = g.GenderTranslations.Single(t => t.LanguageId == currentLanguageId).Name
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<DropDownDto>> GetRelatives(short currentLanguageId)
        {
            try
            {
                return await _repositoryManager.RelativeRepository.FindAll(false)
                    .Select(r =>
                    new DropDownDto
                    {
                        Id = r.RelativeId,
                        Name = r.RelativeTranslations.Single(t => t.LanguageId == currentLanguageId).Name
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<AcademyCoursesDto> GetAcademyCoursesToday(
        //    long academyId, short currentLanguage)
        //{
        //    try
        //    {
        //        return await GetAcademyCoursesInDate(
        //            academyId, currentLanguage,
        //            DateOnly.FromDateTime(DateTime.Today).ToString(XsportConstants.DateOnlyFormat));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        //public async Task<AcademyCoursesDto> GetAcademyCoursesInDate(
        //    long academyId, short currentLanguageId, string targetDate)
        //{
        //    try
        //    {
        //        DateOnly date = DateOnly.Parse(targetDate);
        //        return await _repositoryManager.AcademyRepository
        //            .FindByCondition(a => a.AcademyId == academyId, false).Select(a => new AcademyCoursesDto()
        //            {
        //                AcademyId = a.AcademyId,
        //                AcademyName = a.AcademyTranslations
        //                .Single(t => t.LanguageId == currentLanguageId).Name,
        //                CoverVideo = a.Mutimedias.Single(m => m.IsVideo && m.IsCover).FilePath,
        //                CoverPhoto = a.Mutimedias.Single(m => !m.IsVideo && m.IsCover).FilePath,
        //                Photos = a.Mutimedias.Where(m => !m.IsVideo && !m.IsCover)
        //                .Select(p => p.FilePath),
        //                Videos = a.Mutimedias.Where(m => m.IsVideo && !m.IsCover)
        //                .Select(p => p.FilePath),
        //                Date = DateOnly.FromDateTime(DateTime.Today).ToString(XsportConstants.DateOnlyFormat),
        //                AgeCategoriesWithCoursesInDate = a.AgeCategorys
        //                .Select(ac => new AgeCategoriesWithCoursesInDate()
        //                {
        //                    AgeCategoryId = ac.AgeCategoryId,
        //                    AgeCategoryName = ac.AgeCategoryTranslations
        //                    .Single(t => t.LanguageId == currentLanguageId).Name,
        //                    FromAge = ac.FromAge,
        //                    ToAge = ac.ToAge,
        //                    Courses = ac.Courses
        //                    .Where(c => c.AcademyId == a.AcademyId)
        //                    .Where(c => c.StartDate <= date && c.EndDate >= date)
        //                    .Where(c => c.CourseWorkingDays.Select(w => w.WorkingDay.OrderInWeek)
        //                    .Contains((int)date.DayOfWeek))
        //                    .Select(c => new CoursesDto()
        //                    {
        //                        CourseId = c.CourseId,
        //                        CourseName = c.CourseTranslations
        //                        .Single(t => t.LanguageId == currentLanguageId).Name,
        //                        Description = c.CourseTranslations
        //                        .Single(t => t.LanguageId == currentLanguageId).Description,
        //                        StartTime = c.CourseWorkingDays.Single(w => w.WorkingDay.OrderInWeek == (int)date.DayOfWeek).StartAt.ToString(XsportConstants.TimeOnlyFormat),
        //                        EndTime = c.CourseWorkingDays.Single(w => w.WorkingDay.OrderInWeek == (int)date.DayOfWeek).EndAt.ToString(XsportConstants.TimeOnlyFormat),
        //                        SportId = c.SportId,
        //                        SportName = c.Sport.SportTranslations
        //                        .Single(t => t.LanguageId == currentLanguageId).Name,
        //                    })
        //                })
        //            }).FirstAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<AcademyReviewDto> GetAcademyReviews(
            long academyId, short currentLanguageId)
        {
            try
            {
                string domainName = httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
                return await _repositoryManager.AcademyRepository
                    .FindByCondition(a => a.AcademyId == academyId, false).Select(a => new AcademyReviewDto
                    {
                        AcademyId = a.AcademyId,
                        AcademyName = a.AcademyTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                        CoverPhoto = a.Mutimedias.Single(m => !m.IsVideo && m.IsCover).FilePath,
                        CoverVideo = a.Mutimedias.Single(m => m.IsVideo && m.IsCover).FilePath,
                        Photos = a.Mutimedias.Where(m => !m.IsVideo && !m.IsCover).Select(p => p.FilePath),
                        Videos = a.Mutimedias.Where(m => m.IsVideo && !m.IsCover).Select(p => p.FilePath),
                        NumReviews = a.AcademyReviews.Count,
                        TotalEvaluation = (a.AcademyReviews.Count == 0) ?
                        0 : a.AcademyReviews.Select(r => r.Evaluation).Average(),
                        Reviews = a.AcademyReviews.Select(r => new ReviewDto
                        {
                            ReviewId = r.AcademyReviewId,
                            UserId = r.XsportUserId,
                            UserName = r.XsportUser.XsportName ?? string.Empty,
                            UserImg = string.IsNullOrEmpty(r.XsportUser.ImagePath) ? ""
                            : domainName + "/Images/" + r.XsportUser.ImagePath,
                            ReviewContent = r.Description,
                            Evaluation = r.Evaluation,
                            ReviewDateTime = r.ReviewDateTime.ToString(XsportConstants.DateTimeFormat),
                        })
                    }).FirstAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> AddWorkingDays(AddWorkingDaysDto dto)
        {
            try
            {
                return await _repositoryManager.AcademyRepository.AddWorkingDays(dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
