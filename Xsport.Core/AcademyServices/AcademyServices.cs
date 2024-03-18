using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xsport.Common.Constants;
using Xsport.Common.Enums;
using Xsport.Common.Utils;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DTOs.AcademyDtos;
using Xsport.DTOs.AdminDtos;
using Xsport.DTOs.CommonDtos;

namespace Xsport.Core.AcademyServices
{
    public class AcademyServices : IAcademyServices
    {
        private IRepositoryManager _repositoryManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AcademyServices(
            IRepositoryManager repositoryManager, IWebHostEnvironment _webHostEnvironment)
        {
            _repositoryManager = repositoryManager;
            webHostEnvironment = _webHostEnvironment;

        }

        public async Task<List<UserSportsAcademies>> GetSportsMemberShip(long uId, short currentLanguageId)
        {
            var userSportsAcademies = _repositoryManager.AcademyRepository
                .GetUserSportsAcademies(uId, currentLanguageId);
            var SportsAcademiesGroupedBySportId = userSportsAcademies.GroupBy(x => x.SportId);
            List<long> sportsIds = new List<long>();
            List<UserSportsAcademies> sportsAcademies = new List<UserSportsAcademies>();
            foreach (var group in SportsAcademiesGroupedBySportId)
            {
                sportsIds.Add(group.Key);
                SportInfo sportInfo = new SportInfo();
                List<AcademyInfo> academiesInfo = new List<AcademyInfo>();
                foreach (var userSportAcademy in group)
                {
                    academiesInfo.Add(new AcademyInfo()
                    {
                        AcademyId = userSportAcademy.AcademyId,
                        Name = userSportAcademy.AcademyName,
                        Points = userSportAcademy.UserPoints
                    });
                    sportInfo = new SportInfo()
                    {
                        SportId = userSportAcademy.SportId,
                        Name = userSportAcademy.SportName
                    };
                }
                sportsAcademies.Add(new UserSportsAcademies()
                {
                    SportInfo = sportInfo,
                    AcademyInfoes = academiesInfo
                });
            }
            var restSports = await _repositoryManager.SportRepository
                .FindByCondition(s => !sportsIds.Contains(s.SportId), false)
                .Select(s => new UserSportsAcademies()
                {
                    SportInfo = new SportInfo()
                    {
                        SportId = s.SportId,
                        Name = s.SportTranslations
                        .Single(t => t.LanguageId == currentLanguageId).Name,
                    },
                    AcademyInfoes = new List<AcademyInfo>()
                }).ToListAsync();
            sportsAcademies.AddRange(restSports.OrderBy(r => r.SportInfo.SportId));
            return sportsAcademies;
        }
        public async Task<List<SuggestedAcademyDto>> GetSuggestedAcademies(
            XsportUser user, PagingDto dto, short currentLanguageId)
        {
            try
            {
                var academies = _repositoryManager.AcademyRepository
                    .GetSuggestedAcademies(currentLanguageId,
                    SuggestedAcademiesOrderOptions.EvaluationDown,
                    SuggestedAcademiesFilterOptions.None, string.Empty,
                    dto.PageNumber, dto.PageSize);
                var suggestedAcademies = await academies
                    .Where(a => Utils.CalculateDistanceBetweenTowUsers(
                        user.Latitude ?? 0, user.Longitude ?? 0, a.Lat, a.Long)
                    <= XsportConstants.SameAreaRaduis).ToListAsync();
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
                return await _repositoryManager.AcademyRepository.GetAboutAcademy(
                academyId, currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<AcademyCoursesDto> GetAcademyCoursesToday(
            long academyId, short currentLanguage)
        {
            try
            {
                return await _repositoryManager.AcademyRepository
                .GetCoursesInDate(academyId, currentLanguage, DateOnly.FromDateTime(DateTime.Today));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<AcademyCoursesDto> GetAcademyCoursesInDate(
            long academyId, short currentLanguage, string targetDate)
        {
            try
            {
                DateOnly date = DateOnly.Parse(targetDate);
                return await _repositoryManager.AcademyRepository
                    .GetCoursesInDate(academyId, currentLanguage, date);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AcademyReviewDto> GetAcademyReviews(
            long academyId, short currentLanguageId)
        {
            try
            {
                return await _repositoryManager.AcademyRepository.GetAcademyReviews(
                    academyId, currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddAcademy(AddAcademyDto dto)
        {
            try
            {
                string coverPhotoPath = string.Empty;
                if (dto.CoverPhoto != null)
                    coverPhotoPath = await Utils.UploadImageFileAsync(
                        dto.CoverPhoto, 10, webHostEnvironment);
                string coverVideoPath = string.Empty;
                if (dto.CoverVideo != null)
                    coverVideoPath = await Utils.UploadImageFileAsync(
                        dto.CoverVideo, 10, webHostEnvironment);
                List<string> PhotosPathes = new List<string>();
                if (dto.Photos != null && dto.Photos.Count > 0)
                    foreach (var photo in dto.Photos)
                    {
                        string photoPath = await Utils.UploadImageFileAsync(
                            photo, 10, webHostEnvironment);
                        PhotosPathes.Add(photoPath);
                    }
                List<string> videosPathes = new List<string>();
                if (dto.Videos != null && dto.Videos.Count > 0)
                    foreach (var video in dto.Videos)
                    {
                        string videoPath = await Utils.UploadImageFileAsync(
                            video, 10, webHostEnvironment);
                        videosPathes.Add(videoPath);
                    }
                await _repositoryManager.AcademyRepository
                    .AddAcademy(dto, coverPhotoPath, coverVideoPath, PhotosPathes, videosPathes);
                return true;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddService(AddServiceDto dto)
        {
            try
            {
                return await _repositoryManager.AcademyRepository.AddService(dto);
            }
            catch(Exception ex) 
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
        public async Task<List<GetWorkingDayDto>> GetWorkingDays(short currentLanguageId)
        {
            try
            {
                return await _repositoryManager.AcademyRepository.GetWorkingDays(currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<GetServicesDto>> GetServices(short currentLanguageId)
        {
            try
            {
                return await _repositoryManager.AcademyRepository.GetServices(currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AgeCategory> AddAgeCategory(AddAgeCategoryDto dto)
        {
            try
            {
                return await _repositoryManager.AcademyRepository.AddAgeCategory(dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Course> AddCourse(AddCourseDto dto)
        {
            try
            {
                return await _repositoryManager.AcademyRepository.AddCourse(dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InrollUserInCourse(long uId, long courseId, bool isPersonal)
        {
            try
            {
                return await _repositoryManager.AcademyRepository.InrollUserInCourse(uId, courseId, isPersonal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddServiceToAcademy(long academyId, long serviceValueId)
        {
            try
            {
                return await _repositoryManager.AcademyRepository.AddServiceToAcademy(academyId, serviceValueId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
