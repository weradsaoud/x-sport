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
using Xsport.DTOs.AcademyDtos.MNGDtos;

namespace Xsport.Core.MNGServices.AcademyMNGServices
{
    public class AcademyMNGService : IAcademyMNGService
    {
        private IRepositoryManager _repManager { get; set; }
        private readonly IWebHostEnvironment webHostEnvironment;
        public AcademyMNGService(
            IRepositoryManager repManager,
            IWebHostEnvironment _webHostEnvironment)
        {
            _repManager = repManager;
            webHostEnvironment = _webHostEnvironment;
        }

        public async Task<long> CreateAcademy(PostAcademyDto dto)
        {
            try
            {
                Academy academy = new Academy()
                {
                    AcademyTranslations = new List<AcademyTranslation>
                    {
                        new AcademyTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Name = dto.ArName,
                            Description = dto.ArDescription,
                        },
                        new AcademyTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.English,
                            Name = dto.EnName,
                            Description = dto.EnDescription,
                        }
                    },
                    Phone = dto.Phone,
                    OpenAt = TimeOnly.Parse(dto.OpenAt),
                    CloseAt = TimeOnly.Parse(dto.CloseAt),
                    Lattitude = dto.Lattitude,
                    Longitude = dto.Longitude,
                };
                await _repManager.AcademyRepository.CreateAsync(academy);
                await _repManager.AcademyRepository.SaveChangesAsync();
                return academy.AcademyId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddAcademyMultimedia(AcademyMultimediaDto dto)
        {
            try
            {
                Academy? academy = await _repManager.AcademyRepository
                    .FindByCondition(a => a.AcademyId == dto.AcademyId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Academy does not exsit.");
                string coverPhoto = (dto.CoverPhoto == null) ? string.Empty
                    : await Utils.UploadImageFileAsync(dto.CoverPhoto, academy.AcademyId, webHostEnvironment);
                if (!string.IsNullOrEmpty(coverPhoto))
                    await _repManager.MutimediaRepository.CreateAsync(new Mutimedia
                    {
                        AcademyId = dto.AcademyId,
                        FilePath = coverPhoto,
                        IsCover = true,
                        IsVideo = false,
                    });
                string coverVideo = (dto.CoverVideo == null) ? string.Empty
                    : await Utils.UploadImageFileAsync(dto.CoverVideo, academy.AcademyId, webHostEnvironment);
                if (!string.IsNullOrEmpty(coverVideo))
                    await _repManager.MutimediaRepository.CreateAsync(new Mutimedia
                    {
                        AcademyId = dto.AcademyId,
                        FilePath = coverVideo,
                        IsCover = true,
                        IsVideo = true,
                    });
                if (dto.Photos != null)
                    foreach (var photo in dto.Photos)
                    {
                        string photoPath = (photo == null) ? string.Empty
                            : await Utils.UploadImageFileAsync(photo, dto.AcademyId, webHostEnvironment);
                        if (!string.IsNullOrEmpty(photoPath))
                            await _repManager.MutimediaRepository.CreateAsync(new Mutimedia
                            {
                                AcademyId = dto.AcademyId,
                                FilePath = photoPath,
                                IsCover = false,
                                IsVideo = false,
                            });
                    }
                if (dto.Videos != null)
                    foreach (var video in dto.Videos)
                    {
                        string videoPath = (video == null) ? string.Empty
                            : await Utils.UploadImageFileAsync(video, dto.AcademyId, webHostEnvironment);
                        if (!string.IsNullOrEmpty(videoPath))
                            await _repManager.MutimediaRepository.CreateAsync(new Mutimedia
                            {
                                AcademyId = dto.AcademyId,
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
        public async Task<bool> AddAcademyServices(AcademyServicesDto dto)
        {
            try
            {
                Academy? academy = await _repManager.AcademyRepository
                    .FindByCondition(a => a.AcademyId == dto.AcademyId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Academy does not exist.");
                foreach (var serviceId in dto.servicesIds)
                {
                    Service? service = await _repManager.ServiceRepository
                        .FindByCondition(s => s.ServiceId == serviceId, false)
                        .SingleOrDefaultAsync()
                        ?? throw new Exception($"Service with id = {serviceId} does not exist.");
                    await _repManager.AcademyServiceRepository.CreateAsync(new AcademyService
                    {
                        AcademyId = dto.AcademyId,
                        ServiceId = serviceId,
                    });
                }
                await _repManager.AcademyServiceRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddAcademyWorkingDays(AcademyWorkingDaysDto dto)
        {
            try
            {
                Academy? academy = await _repManager.AcademyRepository
                    .FindByCondition(a => a.AcademyId == dto.AcademyId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Academy does not exist.");
                foreach (var day in dto.AcademyWorkingDays)
                {
                    WorkingDay workingDay = await _repManager.WorkingDayRepository
                        .FindByCondition(w => w.WorkingDayId == day.WorkingDayId, false)
                        .SingleOrDefaultAsync()
                        ?? throw new Exception($"WokingDay with id = {day.WorkingDayId} does not exist.");
                    AcademyWorkingDay academyWorkingDay = new AcademyWorkingDay()
                    {
                        AcademyId = academy.AcademyId,
                        WorkingDayId = day.WorkingDayId,
                        OpenAt = TimeOnly.Parse(day.OpenAt),
                        CloseAt = TimeOnly.Parse(day.CloseAt)
                    };
                    await _repManager.AcademyWorkingDayRepository
                        .CreateAsync(academyWorkingDay);
                }
                await _repManager.AcademyWorkingDayRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
