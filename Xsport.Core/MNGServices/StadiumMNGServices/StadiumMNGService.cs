using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.Common.Utils;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DTOs.StadiumDtos.MNGDtos;

namespace Xsport.Core.MNGServices.StadiumMNGServices
{
    public class StadiumMNGService : IStadiumMNGService
    {
        public IRepositoryManager _repManager { get; set; }
        private readonly IWebHostEnvironment webHostEnvironment;
        public StadiumMNGService(
            IRepositoryManager repManager,
            IWebHostEnvironment _webHostEnvironment)
        {
            _repManager = repManager;
            webHostEnvironment = _webHostEnvironment;
        }
        public async Task<long> CreateStadium(StadiumDto dto)
        {
            try
            {
                Academy? academy = dto.AcademyId.HasValue ? await _repManager.AcademyRepository
                    .FindByCondition(a => a.AcademyId == dto.AcademyId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Academy does not exist.") : null;
                Stadium stadium = new Stadium()
                {
                    StadiumTranslations = new List<StadiumTranslation>()
                {
                    new StadiumTranslation()
                    {
                        LanguageId = (long)LanguagesEnum.Arabic,
                        Name = dto.ArName,
                        Description = dto.ArDescription,
                    },
                    new StadiumTranslation()
                    {
                        LanguageId = (long)LanguagesEnum.English,
                        Name = dto.EnName,
                        Description = dto.EnDescription,
                    }
                },
                    AcademyId = dto.AcademyId,
                    Latitude = dto.Lat,
                    Longitude = dto.Long,
                    Price = dto.Price
                };
                await _repManager.StadiumRepository.CreateAsync(stadium);
                await _repManager.StadiumRepository.SaveChangesAsync();
                return stadium.StadiumId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddStadiumMultimedia(StadiumMultimediaDto dto)
        {
            try
            {
                Stadium? stadium = await _repManager.StadiumRepository
                    .FindByCondition(s => s.StadiumId == dto.StadiumId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Stadium does not exist.");
                string coverPhoto = (dto.CoverPhoto == null) ? string.Empty
                    : await Utils.UploadImageFileAsync(dto.CoverPhoto, stadium.StadiumId, webHostEnvironment);
                if (!string.IsNullOrEmpty(coverPhoto))
                    await _repManager.MutimediaRepository.CreateAsync(new Mutimedia
                    {
                        StadiumId = dto.StadiumId,
                        FilePath = coverPhoto,
                        IsCover = true,
                        IsVideo = false,
                    });
                string coverVideo = (dto.CoverVideo == null) ? string.Empty
                    : await Utils.UploadImageFileAsync(dto.CoverVideo, stadium.StadiumId, webHostEnvironment);
                if (!string.IsNullOrEmpty(coverVideo))
                    await _repManager.MutimediaRepository.CreateAsync(new Mutimedia
                    {
                        StadiumId = dto.StadiumId,
                        FilePath = coverVideo,
                        IsCover = true,
                        IsVideo = true,
                    });
                if (dto.Photos != null)
                    foreach (var photo in dto.Photos)
                    {
                        string photoPath = (photo == null) ? string.Empty
                            : await Utils.UploadImageFileAsync(photo, dto.StadiumId, webHostEnvironment);
                        if (!string.IsNullOrEmpty(photoPath))
                            await _repManager.MutimediaRepository.CreateAsync(new Mutimedia
                            {
                                StadiumId = dto.StadiumId,
                                FilePath = photoPath,
                                IsCover = false,
                                IsVideo = false,
                            });
                    }
                if (dto.Videos != null)
                    foreach (var video in dto.Videos)
                    {
                        string videoPath = (video == null) ? string.Empty
                            : await Utils.UploadImageFileAsync(video, dto.StadiumId, webHostEnvironment);
                        if (!string.IsNullOrEmpty(videoPath))
                            await _repManager.MutimediaRepository.CreateAsync(new Mutimedia
                            {
                                StadiumId = dto.StadiumId,
                                FilePath = videoPath,
                                IsCover = false,
                                IsVideo = true,
                            });
                    }
                await _repManager.MutimediaRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddStadiumServices(StadiumServicesDto dto)
        {
            try
            {
                Stadium? stadium = await _repManager.StadiumRepository
                    .FindByCondition(s => s.StadiumId == dto.StadiumId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Stadium does not exist.");
                foreach (var serviceId in dto.ServicesIds)
                {
                    Service? service = await _repManager.ServiceRepository
                        .FindByCondition(s => s.ServiceId == serviceId, false)
                        .SingleOrDefaultAsync() ?? throw new Exception("Service does not exist.");
                    await _repManager.StadiumServiceRepository.CreateAsync(new StadiumService
                    {
                        StadiumId = dto.StadiumId,
                        ServiceId = serviceId,
                    });
                }
                await _repManager.StadiumServiceRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddStadiumFloor(StadiumFloorDto dto)
        {
            try
            {
                Stadium? stadium = await _repManager.StadiumRepository
                    .FindByCondition(s => s.StadiumId == dto.StadiumId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Stadium does not exist.");
                Floor? floor = await _repManager.FloorRepository
                    .FindByCondition(f => f.FloorId == dto.FloorId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Stadium does not exist.");
                StadiumFloor stadiumFloor = new StadiumFloor()
                {
                    StadiumId = dto.StadiumId,
                    FloorId = floor.FloorId,
                };
                await _repManager.StadiumFloorRepository.CreateAsync(stadiumFloor);
                await _repManager.StadiumFloorRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddStadiumWorkingDays(StadiumWorkingDaysDto dto)
        {
            try
            {
                Stadium? stadium = await _repManager.StadiumRepository
                    .FindByCondition(a => a.StadiumId == dto.StadiumId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Stadium does not exist.");
                foreach (var day in dto.StadiumWorkingDays)
                {
                    WorkingDay workingDay = await _repManager.WorkingDayRepository
                        .FindByCondition(w => w.WorkingDayId == day.WorkingDayId, false)
                        .SingleOrDefaultAsync()
                        ?? throw new Exception($"WokingDay with id = {day.WorkingDayId} does not exist.");
                    StadiumWorkingDay stadiumWorkingDay = new StadiumWorkingDay()
                    {
                        StadiumId = stadium.StadiumId,
                        WorkingDayId = day.WorkingDayId,
                        OpenAt = TimeOnly.Parse(day.OpenAt),
                        CloseAt = TimeOnly.Parse(day.CloseAt)
                    };
                    await _repManager.StadiumWorkingDayRepositpry
                        .CreateAsync(stadiumWorkingDay);
                }
                await _repManager.StadiumWorkingDayRepositpry.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
