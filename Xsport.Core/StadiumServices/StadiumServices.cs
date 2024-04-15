using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Constants;
using Xsport.Common.Enums;
using Xsport.Common.Utils;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DB.QueryObjects;
using Xsport.DTOs;
using Xsport.DTOs.StadiumDtos;
using StadiumService = Xsport.DTOs.StadiumDtos.StadiumService;

namespace Xsport.Core.StadiumServices
{
    public class StadiumServices : IStadiumServices
    {
        private IRepositoryManager _repManager { get; set; }
        private readonly IHttpContextAccessor httpContextAccessor;
        public StadiumServices(IRepositoryManager repManager, IHttpContextAccessor _httpContextAccessor)
        {
            _repManager = repManager;
            httpContextAccessor = _httpContextAccessor;
        }

        public async Task<List<SuggestedStadiumDto>> GetFriendsStadiums(
            long uId, long sportId, int pageNum, int pageSize, short currentLanguageId)
        {
            try
            {
                string domainName = httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
                return await _repManager.StadiumRepository.FindAll(false)
                    .MapStadiumToSuggested(currentLanguageId, domainName, sportId)
                    .FilterSuggestedStadiums(SuggestedStadiumsFilterOptions.SportId, sportId.ToString())
                    .OrderSuggestedStadiums(SuggestedStadiumsOrderOptions.EvaluationDown)
                    .Page<SuggestedStadiumDto>(pageNum, pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<SuggestedStadiumDto>> GetNearByStadiums(
            long uId, long sportId, int pageNum, int pageSize, short currentLanguageId)
        {
            try
            {
                string domainName = httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
                XsportUser user = await _repManager.UserRepository.FindByCondition(u => u.Id == uId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("User does not exist.");
                var stadiums = await _repManager.StadiumRepository.FindAll(false)
                    .MapStadiumToSuggested(currentLanguageId, domainName, sportId)
                    .FilterSuggestedStadiums(SuggestedStadiumsFilterOptions.SportId, sportId.ToString())
                    .OrderSuggestedStadiums(SuggestedStadiumsOrderOptions.EvaluationDown)
                    .ToListAsync();
                return stadiums.Where(s => Utils.CalculateDistanceBetweenTowUsers(
                        (decimal)user.Latitude, (decimal)user.Longitude, s.Lat, s.Long)
                    <= XsportConstants.SameAreaRaduis).Skip(pageNum * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<AboutStadiumDto> GetAboutStadium(long stadiumId, short currentLanguageId)
        {
            try
            {
                string domainName = httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
                return await _repManager.StadiumRepository
                    .FindByCondition(s => s.StadiumId == stadiumId, false)
                    .Select(s => new AboutStadiumDto()
                    {
                        StadiumId = s.StadiumId,
                        Name = s.StadiumTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                        Description = s.StadiumTranslations.Single(t => t.LanguageId == currentLanguageId).Description,
                        Floors = s.StadiumFloors.Select(sf => new DropDownDto()
                        {
                            Id = sf.FloorId,
                            Name = sf.Floor.FloorTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                        }).ToList(),
                        Price = s.Price,
                        OpenAt = s.StadiumWorkingDays
                        .Single(wd => wd.WorkingDay.OrderInWeek == (int)DateTime.Today.DayOfWeek)
                        .OpenAt.ToString(XsportConstants.TimeOnlyFormat),
                        CloseAt = s.StadiumWorkingDays
                        .Single(wd => wd.WorkingDay.OrderInWeek == (int)DateTime.Today.DayOfWeek)
                        .CloseAt.ToString(XsportConstants.TimeOnlyFormat),
                        Services = s.StadiumServices.Select(s => new StadiumService()
                        {
                            ServiceId = s.ServiceId,
                            ServiceName = s.Service.ServiceTranslations
                            .Single(t => t.LanguageId == currentLanguageId).Name,
                            ImgUrl = string.IsNullOrEmpty(s.Service.ImgPath) ? ""
                            : domainName + "/Images/" + s.Service.ImgPath
                        }).ToList(),
                        CoverPhoto = string.IsNullOrEmpty(s.Mutimedias.Single(m => !m.IsVideo && m.IsCover).FilePath) ? ""
                        : domainName + "/Images/" + s.Mutimedias.Single(m => !m.IsVideo && m.IsCover).FilePath,
                        CoverVideo = string.IsNullOrEmpty(s.Mutimedias.Single(m => m.IsVideo && m.IsCover).FilePath) ? ""
                        : domainName + "/Images/" + s.Mutimedias.Single(m => !m.IsVideo && m.IsCover).FilePath,
                        Photos = s.Mutimedias.Where(m => !m.IsVideo && !m.IsCover).Select(m =>
                        string.IsNullOrEmpty(m.FilePath) ? "" : domainName + "/Images/" + m.FilePath).ToList(),
                        Videos = s.Mutimedias.Where(m => m.IsVideo && !m.IsCover).Select(m =>
                        string.IsNullOrEmpty(m.FilePath) ? "" : domainName + "/Images/" + m.FilePath).ToList(),
                    }).SingleOrDefaultAsync() ?? throw new Exception("Stadium does not exist.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
