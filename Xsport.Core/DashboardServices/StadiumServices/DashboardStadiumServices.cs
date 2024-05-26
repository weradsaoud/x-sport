using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.Common.Utils;
using Xsport.Core.MNGServices.StadiumMNGServices;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DTOs.StadiumDtos.DashboardDtos;
using Xsport.DTOs.StadiumDtos.MNGDtos;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
namespace Xsport.Core.DashboardServices.StadiumServices
{
    public class DashboardStadiumServices : IDashboardStadiumServices
    {
        public IRepositoryManager _repManager { get; set; }
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IStadiumMNGService _stadiumMNGService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardStadiumServices(
            IRepositoryManager repManager,
            IStadiumMNGService stadiumMNGService,
            IWebHostEnvironment _webHostEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            _repManager = repManager;
            _stadiumMNGService = stadiumMNGService;
            webHostEnvironment = _webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<StadiumProcessCreationDto>> GetStadiumCreatopnProcesses(long userId)
        {
            try
            {
                string domainName = _httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + _httpContextAccessor.HttpContext?.Request.Host.Value;

                List<StadiumCreationProcess> existingProcess = _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.UserId == userId, false)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.StadiumTranslations)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.StadiumFloors)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.StadiumServices)
                    .Include( s => s.Stadium)
                    .ThenInclude(z => z.StadiumWorkingDays)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.Mutimedias).ToList();

                List<StadiumProcessCreationDto> stadiumStepDataModels = new List<StadiumProcessCreationDto>();
                if (existingProcess != null && existingProcess.Count > 0)
                {

                    existingProcess.ForEach(p =>
                    {
                        var services = new DashboardStadiumServicesDto() { ServicesIds = new List<long>() };
                        p.Stadium.StadiumServices?.ToList().ForEach(x => services.ServicesIds.Add(x.ServiceId));
                        stadiumStepDataModels.Add(
                            new StadiumProcessCreationDto()
                            {
                                ProcessId = p.Id,
                                CurrentStep = p.CurrentStep,
                                DashboardData = new StadiumDashboardDto()
                                {
                                    Floors = p.Stadium.StadiumFloors?.Adapt<List<DashboardStadiumFloorDto>>(),
                                    GenerealInfo = new DashboardStadiumDto()
                                    {
                                        ArDescription = p.Stadium.StadiumTranslations?.Where(x => x.LanguageId == (short)LanguagesEnum.Arabic).FirstOrDefault()?.Description,
                                        ArName = p.Stadium.StadiumTranslations?.Where(x => x.LanguageId == (short)LanguagesEnum.Arabic).FirstOrDefault()?.Name,
                                        EnDescription = p.Stadium.StadiumTranslations?.Where(x => x.LanguageId == (short)LanguagesEnum.English).FirstOrDefault()?.Description,
                                        EnName = p.Stadium.StadiumTranslations?.Where(x => x.LanguageId == (short)LanguagesEnum.English).FirstOrDefault()?.Name,
                                        Lat = p.Stadium.Latitude,
                                        Long = p.Stadium.Longitude
                                    },
                                    CoverPhoto = string.IsNullOrEmpty(p.Stadium.Mutimedias?.SingleOrDefault(m => !m.IsVideo && m.IsCover)?.FilePath) ? ""
                        : domainName + "/Images/" + p.Stadium.Mutimedias?.SingleOrDefault(m => !m.IsVideo && m.IsCover)?.FilePath,
                                    CoverVideo = string.IsNullOrEmpty(p.Stadium.Mutimedias?.SingleOrDefault(m => m.IsVideo && m.IsCover)?.FilePath) ? ""
                        : domainName + "/Images/" + p.Stadium.Mutimedias?.SingleOrDefault(m => !m.IsVideo && m.IsCover)?.FilePath,
                                    Photos = p.Stadium.Mutimedias?.Where(m => !m.IsVideo && !m.IsCover).Select(m =>
                                    string.IsNullOrEmpty(m.FilePath) ? "" : domainName + "/Images/" + m.FilePath).ToList(),
                                    Videos = p.Stadium.Mutimedias?.Where(m => m.IsVideo && !m.IsCover).Select(m =>
                                    string.IsNullOrEmpty(m.FilePath) ? "" : domainName + "/Images/" + m.FilePath).ToList(),
                                    Services = services,
                                    WorkingDays = p.Stadium.StadiumWorkingDays?.Adapt<List<DashboardStadiumWorkingDayDto>>(),
                                }
                            });
                    });

                    return stadiumStepDataModels;
                }

                var newProcess = new StadiumCreationProcess
                {
                    UserId = userId,
                    CurrentStep = "B2",
                    LastUpdated = DateTime.UtcNow,
                    Stadium = new Stadium()
                    {
                        IsComplete = false
                    }
                };

                _repManager.StadiumCreationProcessRepository.Create(newProcess);
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                stadiumStepDataModels.Add(
                        new StadiumProcessCreationDto()
                        {
                            ProcessId = newProcess.Id,
                            CurrentStep = "B2",
                            DashboardData = new StadiumDashboardDto(),
                        });
                return stadiumStepDataModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //public async Task<StadiumStepDataModel> SaveStep(long userId, StadiumStepDataModel model)
        //{
        //    try
        //    {
        //        var process = await _repManager.StadiumCreationProcessRepository
        //            .FindByCondition(p => p.Id == model.ProcessId && p.UserId == userId, false)
        //            .Include(x => x.Stadium)
        //            .ThenInclude(x => x.StadiumTranslations)
        //            .Include(x => x.Stadium)
        //            .ThenInclude(x => x.StadiumFloors)
        //            .Include(x => x.Stadium)
        //            .ThenInclude(x => x.StadiumServices)
        //            .Include(x => x.Stadium)
        //            .ThenInclude(x => x.Mutimedias)
        //                .FirstOrDefaultAsync();

        //        if (process == null)
        //        {
        //            throw new Exception("You are not signed in.");
        //        }
        //        process.LastUpdated = DateTime.UtcNow;

        //        _repManager.StadiumCreationProcessRepository.Update(process);
        //        await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();

        //        return model;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<bool> CompleteCreationProcess(long userId, long processId)
        {
            try
            {
                var process = await _repManager.StadiumCreationProcessRepository.FindByCondition(p => p.Id == processId && p.UserId == userId, true).Include(x => x.Stadium)
                           .FirstOrDefaultAsync();
                if (process == null) { throw new Exception("Stadium doesn't exist"); }
                process.Stadium.IsComplete = true;
                await _repManager.StadiumRepository.SaveChangesAsync();
                _repManager.StadiumCreationProcessRepository.Delete(process);
                _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddStadiumDecription(DashboardStadiumDiscriptionDto dto, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.Id == processId && p.UserId == userId, true)
                    .Include(x => x.Stadium)
                    .ThenInclude(s => s.StadiumTranslations)
                           .FirstOrDefaultAsync();

                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }

                if (dto.EnDescription != null && dto.ArDescription != null)
                {
                    var arTranslation = CreationProcess.Stadium.StadiumTranslations?.FirstOrDefault(t => t.LanguageId == (long)LanguagesEnum.Arabic);

                    if (arTranslation != null)
                    {
                        arTranslation.Description = dto.ArDescription;
                    }
                    else
                    {
                        CreationProcess.Stadium.StadiumTranslations = new List<StadiumTranslation>();
                        CreationProcess.Stadium.StadiumTranslations.Add(new StadiumTranslation
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Description = dto.ArDescription,
                        });
                    }
                    var enTranslation = CreationProcess.Stadium.StadiumTranslations.FirstOrDefault(t => t.LanguageId == (long)LanguagesEnum.English);

                    if (enTranslation != null)
                    {
                        enTranslation.Description = dto.EnDescription;
                    }
                    else
                    {
                        CreationProcess.Stadium.StadiumTranslations.Add(new StadiumTranslation
                        {
                            LanguageId = (long)LanguagesEnum.English,
                            Description = dto.EnDescription,
                        });
                    }
                    CreationProcess.CurrentStep = "B3";
                }
                await _repManager.StadiumRepository.SaveChangesAsync();
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddStadiumLocation(DashboardStadiumLocationDto dto, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository.FindByCondition(p => p.Id == processId && p.UserId == userId, true).Include(x => x.Stadium)
                           .FirstOrDefaultAsync();
                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }

                if (dto.Latitude != null && dto.Longitude != null)
                {
                    CreationProcess.Stadium.Latitude = dto.Latitude;
                    CreationProcess.Stadium.Longitude = dto.Longitude;
                    CreationProcess.CurrentStep = "B4";
                }
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<FloorDto>> GetSportsFloors(long sportId)
        {
            try
            {
                var floors = await _repManager.FloorRepository.FindByCondition(s => s.SportId == sportId, false).ToListAsync();
                var floorsDto = new List<FloorDto>();
                foreach (var floor in floors)
                {
                    floorsDto.Add(new FloorDto()
                    {
                        FloorId = floor.FloorId,
                        NumOfPlayers = floor.NumPlayers
                    });
                }
                return floorsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<ServiceDto>> GetServices(short currentLanguageId)
        {
            try
            {
                var services = await _repManager.ServiceRepository.FindAll(false).Include(a => a.ServiceTranslations).ToListAsync();
                var servicesDto = new List<ServiceDto>();
                foreach (var service in services)
                {
                    servicesDto.Add(new ServiceDto()
                    {
                        ServiceName = service.ServiceTranslations?.Where(s => s.LanguageId == currentLanguageId)?.FirstOrDefault()?.Name ?? "",
                        ServiceId = service.ServiceId,
                        ImgPath = service.ImgPath
                    });
                }
                return servicesDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddStadiumFloors(DashboardStadiumFloorsDto dto, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.Id == processId && p.UserId == userId, true)
                    .Include(x => x.Stadium)
                    .ThenInclude(s => s.StadiumFloors)
                           .FirstOrDefaultAsync();

                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }
                if (CreationProcess.Stadium.StadiumFloors == null)
                {
                    CreationProcess.Stadium.StadiumFloors = new List<StadiumFloor>();
                }
                CreationProcess.Stadium.StadiumFloors?.Clear();
                dto.Floors.ForEach(async f =>
                {
                    Floor? floor = await _repManager.FloorRepository
                    .FindByCondition(x => x.FloorId == f.FloorId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Floor Type does not exist.");
                    StadiumFloor stadiumFloor = new StadiumFloor()
                    {
                        StadiumId = CreationProcess.Stadium.StadiumId,
                        FloorId = floor.FloorId,
                        IsCovered = f.IsCovered
                    };


                    CreationProcess.Stadium.StadiumFloors.Add(stadiumFloor);
                });
                CreationProcess.CurrentStep = "B7";
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddStadiumServices(DashboardStadiumServicesDto dto, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.Id == processId && p.UserId == userId, true)
                    .Include(x => x.Stadium)
                    .ThenInclude(s => s.StadiumServices)
                          .FirstOrDefaultAsync();

                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }
                if (CreationProcess.Stadium.StadiumServices == null)
                {
                    CreationProcess.Stadium.StadiumServices = new List<StadiumService>();
                }
                foreach (var serviceId in dto.ServicesIds)
                {
                    Service? service = await _repManager.ServiceRepository
                        .FindByCondition(s => s.ServiceId == serviceId, false)
                        .SingleOrDefaultAsync() ?? throw new Exception("Service does not exist.");

                    CreationProcess.Stadium.StadiumServices?.Clear();
                    CreationProcess.Stadium.StadiumServices.Add(new StadiumService
                    {
                        StadiumId = CreationProcess.Stadium.StadiumId,
                        ServiceId = serviceId,
                    });

                }
                CreationProcess.CurrentStep = "B9";
                await _repManager.ServiceRepository.SaveChangesAsync();
                await _repManager.StadiumRepository.SaveChangesAsync();
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddStadiumName(DashboardStadiumNameDto dto, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.Id == processId && p.UserId == userId, true)
                    .Include(x => x.Stadium)
                    .ThenInclude(s => s.StadiumTranslations)
                           .FirstOrDefaultAsync();

                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }

                if (dto.EnName != null && dto.ArName != null)
                {
                    var arTranslation = CreationProcess.Stadium.StadiumTranslations?.FirstOrDefault(t => t.LanguageId == (long)LanguagesEnum.Arabic);

                    if (arTranslation != null)
                    {
                        arTranslation.Name = dto.ArName;
                    }
                    else
                    {
                        CreationProcess.Stadium.StadiumTranslations = new List<StadiumTranslation>();
                        CreationProcess.Stadium.StadiumTranslations.Add(new StadiumTranslation
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Name = dto.ArName,
                        });
                    }
                    var enTranslation = CreationProcess.Stadium.StadiumTranslations?.FirstOrDefault(t => t.LanguageId == (long)LanguagesEnum.English);

                    if (enTranslation != null)
                    {
                        enTranslation.Name = dto.EnName;
                    }
                    else
                    {
                        CreationProcess.Stadium.StadiumTranslations.Add(new StadiumTranslation
                        {
                            LanguageId = (long)LanguagesEnum.English,
                            Name = dto.EnName,
                        });
                    }
                    CreationProcess.CurrentStep = "B10";
                }

                await _repManager.StadiumRepository.SaveChangesAsync();
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> CreateStadium(DashboardStadiumDto dto, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository.FindByCondition(p => p.Id == processId && p.UserId == userId, true).Include(x => x.Stadium)
                           .FirstOrDefaultAsync();
                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }
                if (dto.ArName != null || dto.ArDescription != null)
                {
                    var existingTranslation = CreationProcess.Stadium.StadiumTranslations.FirstOrDefault(t => t.LanguageId == (long)LanguagesEnum.Arabic);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Name = dto.ArName;
                        existingTranslation.Description = dto.ArDescription;
                    }
                    else
                    {
                        CreationProcess.Stadium.StadiumTranslations.Add(new StadiumTranslation
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Name = dto.ArName,
                            Description = dto.ArDescription,
                        });
                    }
                }
                if (dto.EnName != null || dto.EnDescription != null)
                {
                    var existingTranslation = CreationProcess.Stadium.StadiumTranslations.FirstOrDefault(t => t.LanguageId == (long)LanguagesEnum.English);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Name = dto.EnName;
                        existingTranslation.Description = dto.EnDescription;
                    }
                    else
                    {
                        CreationProcess.Stadium.StadiumTranslations.Add(new StadiumTranslation
                        {
                            LanguageId = (long)LanguagesEnum.English,
                            Name = dto.EnName,
                            Description = dto.EnDescription,
                        });
                    }
                }
                CreationProcess.Stadium.Latitude = dto.Lat;
                CreationProcess.Stadium.Longitude = dto.Long;
                //Price = dto.PriceDescription = dto.EnDescription,
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return CreationProcess.Stadium.StadiumId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddStadiumMultimedia(DashboardStadiumMultimediaDto dto, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.Id == processId && p.UserId == userId, true)
                    .Include(x => x.Stadium)
                    .ThenInclude(s => s.Mutimedias)
                           .FirstOrDefaultAsync();

                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }
                string coverPhoto = (dto.CoverPhoto == null) ? string.Empty
                    : await Utils.UploadImageFileAsync(dto.CoverPhoto, CreationProcess.Stadium.StadiumId, webHostEnvironment);
                if (!string.IsNullOrEmpty(coverPhoto))
                {
                    if (CreationProcess.Stadium.Mutimedias == null)
                    {
                        CreationProcess.Stadium.Mutimedias = new List<Mutimedia>();
                        CreationProcess.Stadium.Mutimedias.Clear();
                    }
                    CreationProcess.Stadium.Mutimedias.Add(new Mutimedia
                    {
                        StadiumId = CreationProcess.Stadium.StadiumId,
                        FilePath = coverPhoto,
                        IsCover = true,
                        IsVideo = false,
                    });
                }
                string coverVideo = (dto.CoverVideo == null) ? string.Empty
                    : await Utils.UploadImageFileAsync(dto.CoverVideo, CreationProcess.Stadium.StadiumId, webHostEnvironment);
                if (!string.IsNullOrEmpty(coverVideo))
                    CreationProcess.Stadium.Mutimedias.Add(new Mutimedia
                    {
                        StadiumId = CreationProcess.Stadium.StadiumId,
                        FilePath = coverVideo,
                        IsCover = true,
                        IsVideo = true,
                    });
                if (dto.Photos != null)
                    foreach (var photo in dto.Photos)
                    {
                        string photoPath = (photo == null) ? string.Empty
                            : await Utils.UploadImageFileAsync(photo, CreationProcess.Stadium.StadiumId, webHostEnvironment);
                        if (!string.IsNullOrEmpty(photoPath))
                            CreationProcess.Stadium.Mutimedias.Add(new Mutimedia
                            {
                                StadiumId = CreationProcess.Stadium.StadiumId,
                                FilePath = photoPath,
                                IsCover = false,
                                IsVideo = false,
                            });
                    }
                if (dto.Videos != null)
                    foreach (var video in dto.Videos)
                    {
                        string videoPath = (video == null) ? string.Empty
                            : await Utils.UploadImageFileAsync(video, CreationProcess.Stadium.StadiumId, webHostEnvironment);
                        if (!string.IsNullOrEmpty(videoPath))
                            CreationProcess.Stadium.Mutimedias.Add(new Mutimedia
                            {
                                StadiumId = CreationProcess.Stadium.StadiumId,
                                FilePath = videoPath,
                                IsCover = false,
                                IsVideo = true,
                            });
                    }
                CreationProcess.CurrentStep = "C2";
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddStadiumFloorsPrices(List<DashboardStadiumFloorPriceDto> floors, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.Id == processId && p.UserId == userId, true)
                    .Include(x => x.Stadium)
                    .ThenInclude(s => s.StadiumFloors)
                           .FirstOrDefaultAsync();

                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }

                var stadiumFloors = CreationProcess.Stadium.StadiumFloors.ToList();
                if (stadiumFloors.IsNullOrEmpty()) { throw new Exception("Floors don't exist"); }

                floors.ForEach(f =>
                {
                    var currStadiumFloor = stadiumFloors.Where(sf => sf.StadiumFloorId == f.StadiumFloorId).FirstOrDefault();
                    if (currStadiumFloor == null) { throw new Exception("Stadium Floor don't exist"); }
                    currStadiumFloor.Price = f.Price;
                });
                CreationProcess.CurrentStep = "D3";
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddStadiumWorkingDays(DashboardStadiumWorkingDaysDto dto, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.Id == processId && p.UserId == userId, true)
                    .Include(x => x.Stadium)
                    .ThenInclude(s => s.StadiumWorkingDays)
                           .FirstOrDefaultAsync() ?? throw new Exception("Stadium does not exist.");
                if(CreationProcess.Stadium.StadiumWorkingDays == null)
                {
                    CreationProcess.Stadium.StadiumWorkingDays = new List<StadiumWorkingDay>();
                }
                CreationProcess.Stadium.StadiumWorkingDays.Clear();
                foreach (var day in dto.StadiumWorkingDays)
                {
                    WorkingDay workingDay = await _repManager.WorkingDayRepository
                        .FindByCondition(w => w.WorkingDayId == day.WorkingDayId, false)
                        .SingleOrDefaultAsync()
                        ?? throw new Exception($"WokingDay with id = {day.WorkingDayId} does not exist.");

                    StadiumWorkingDay stadiumWorkingDay = new StadiumWorkingDay()
                    {
                        StadiumId = CreationProcess.StadiumId,
                        WorkingDayId = day.WorkingDayId,
                        OpenAt = TimeOnly.Parse(day.OpenAt),
                        CloseAt = TimeOnly.Parse(day.CloseAt)
                    };
                    await _repManager.StadiumWorkingDayRepositpry
                        .CreateAsync(stadiumWorkingDay);
                }
                CreationProcess.CurrentStep = "D4";
                await _repManager.StadiumWorkingDayRepositpry.SaveChangesAsync();
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddPaymentInfo([FromBody] DashboardPaymentInfoDto dto, long userId, long processId)
        {
            // ToDo Mousa : implement later when payment is ready;
            var CreationProcess = await _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.Id == processId && p.UserId == userId, true)
                    .Include(x => x.Stadium)
                           .FirstOrDefaultAsync() ?? throw new Exception("Stadium does not exist.");

            CreationProcess.CurrentStep = "D7";
            await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();

            return await CompleteCreationProcess(userId, processId);
            
        }

        public async Task<bool> AddReservationType(long reservationType, long userId, long processId)
        {
            var CreationProcess = await _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.Id == processId && p.UserId == userId, true)
                    .Include(x => x.Stadium)
                           .FirstOrDefaultAsync() ?? throw new Exception("Stadium does not exist.");

            CreationProcess.CurrentStep = "D2";
            await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
            return true;
        }
    }
}
