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
        //public async Task<long> AddStadiumStepOne(StadiumStepOneDto step1DTO)
        //{
        //    long stadiumId = await _stadiumMNGService.CreateStadium(step1DTO.GeneralInfo);
        //    step1DTO.Floors?.ForEach(async floor => await _stadiumMNGService.AddStadiumFloor(floor));
        //    step1DTO.Services?.ForEach(async service => await _stadiumMNGService.AddStadiumServices(service));

        //    return stadiumId;
        //}
        public async Task<List<StadiumProcessCreationDto>> GetStadiumCreatopnProcesses(long userId)
        {
            try
            {
                string domainName = _httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + _httpContextAccessor.HttpContext?.Request.Host.Value;

                List<StadiumCreationProcess> existingProcess = _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.UserId == userId && p.CurrentStep < 5, false)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.StadiumTranslations)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.StadiumFloors)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.StadiumServices)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.Mutimedias).ToList();

                List<StadiumProcessCreationDto> stadiumStepDataModels = new List<StadiumProcessCreationDto>();
                if (existingProcess != null && existingProcess.Count > 0)
                {

                    existingProcess.ForEach(p =>
                    {

                        stadiumStepDataModels.Add(
                            new StadiumProcessCreationDto()
                            {
                                ProcessId = p.Id,
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
                                    Services = p.Stadium.StadiumServices?.Adapt<DashboardStadiumServicesDto>(),
                                    WorkingDays = p.Stadium.StadiumWorkingDays?.Adapt<DashboardStaduimWorkingDaysDto>(),
                                }
                            });
                    });

                    return stadiumStepDataModels;
                }

                var newProcess = new StadiumCreationProcess
                {
                    UserId = userId,
                    CurrentStep = 0,
                    LastUpdated = DateTime.UtcNow,
                    Stadium = new Stadium()
                };

                _repManager.StadiumCreationProcessRepository.Create(newProcess);
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                stadiumStepDataModels.Add(
                        new StadiumProcessCreationDto()
                        {
                            ProcessId = newProcess.Id,
                            DashboardData = new StadiumDashboardDto(),
                        });
                return stadiumStepDataModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<StadiumStepDataModel> SaveStep(long userId, StadiumStepDataModel model)
        {
            try
            {
                var process = await _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.Id == model.ProcessId && p.UserId == userId, false)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.StadiumTranslations)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.StadiumFloors)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.StadiumServices)
                    .Include(x => x.Stadium)
                    .ThenInclude(x => x.Mutimedias)
                        .FirstOrDefaultAsync();

                if (process == null)
                {
                    throw new Exception("You are not signed in.");
                }
                //await CreateStadium(model.StadiumData.GenerealInfo, process);
                //if (model.StadiumData.Multimedia != null) await AddStadiumMultimedia(model.StadiumData.Multimedia, process);
                //if (model.StadiumData.Services != null) await AddStadiumServices(model.StadiumData.Services, process);
                //if (model.StadiumData.Floors != null && model.StadiumData.Floors?.Floors?.Count > 0) model.StadiumData.Floors.Floors.ForEach(async f => await AddStadiumFloor(f, process));

                //process.CurrentStep = model.CurrentStep;
                process.LastUpdated = DateTime.UtcNow;

                _repManager.StadiumCreationProcessRepository.Update(process);
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<long> CompleteCreationProcess(long userId, long processId)
        {
            try
            {
                var process = await _repManager.StadiumCreationProcessRepository.FindByCondition(p => p.Id == processId && p.UserId == userId, false).Include(x => x.Stadium)
                           .FirstOrDefaultAsync();
                if (process == null) { throw new Exception("Stadium doesn't exist"); }

                await _repManager.StadiumRepository.CreateAsync(process.Stadium);
                await _repManager.StadiumRepository.SaveChangesAsync();
                return process.Stadium.StadiumId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddStadiumStepTwo(long stadiumId, StadiumStepTwoDto step2DTO)
        {
            //// Retrieve stadium by ID
            //var stadium = await _dbContext.Stadiums.FindAsync(stadiumId);
            //if (stadium == null)
            //{
            //    // Handle error
            //    return;
            //}

            //// Update stadium with step 2 data
            //stadium.Price = step2DTO.Price;
            //// Update other properties

            //await _dbContext.SaveChangesAsync();
        }

        public async Task AddStadiumStepThree(long stadiumId, StadiumStepThreeDto step3DTO)
        {
            //// Retrieve stadium by ID
            //var stadium = await _dbContext.Stadiums.FindAsync(stadiumId);
            //if (stadium == null)
            //{
            //    // Handle error
            //    return;
            //}

            //// Update stadium with step 2 data
            //stadium.Price = step2DTO.Price;
            //// Update other properties

            //await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AddStadiumDecription(DashboardStadiumDiscriptionDto dto, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository.FindByCondition(p => p.Id == processId && p.UserId == userId, false).Include(x => x.Stadium)
                           .FirstOrDefaultAsync();
                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }

                if (dto.EnDescription != null && dto.ArDescription != null)
                {
                    var arTranslation = CreationProcess.Stadium.StadiumTranslations.FirstOrDefault(t => t.LanguageId == (long)LanguagesEnum.Arabic);

                    if (arTranslation != null)
                    {
                        arTranslation.Description = dto.ArDescription;
                    }
                    else
                    {
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
                }
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
                var CreationProcess = await _repManager.StadiumCreationProcessRepository.FindByCondition(p => p.Id == processId && p.UserId == userId, false).Include(x => x.Stadium)
                           .FirstOrDefaultAsync();
                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }

                if (dto.Latitude != null && dto.Longitude != null)
                {
                    CreationProcess.Stadium.Latitude = dto.Latitude;
                    CreationProcess.Stadium.Longitude = dto.Longitude;
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
                var floors =  await _repManager.FloorRepository.FindByCondition(s => s.SportId == sportId, false).ToListAsync();
                var floorsDto = new List<FloorDto>();
                foreach (var floor in floors) {
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
        public async Task<bool> AddStadiumFloors(DashboardStadiumFloorsDto dto, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository.FindByCondition(p => p.Id == processId && p.UserId == userId, false).Include(x => x.Stadium)
                           .FirstOrDefaultAsync();

                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }
                CreationProcess.Stadium.StadiumFloors.Clear();
                dto.FloorIds.ForEach(async fId =>
                {
                    Floor? floor = await _repManager.FloorRepository
                    .FindByCondition(x => x.FloorId == fId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Floor Type does not exist.");
                    StadiumFloor stadiumFloor = new StadiumFloor()
                    {
                        StadiumId = CreationProcess.Stadium.StadiumId,
                        FloorId = floor.FloorId
                    };
                    
                    CreationProcess.Stadium.StadiumFloors.Add(stadiumFloor);
                });
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
                var CreationProcess = await _repManager.StadiumCreationProcessRepository.FindByCondition(p => p.Id == processId && p.UserId == userId, false).Include(x => x.Stadium)
                          .FirstOrDefaultAsync();

                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }

                foreach (var serviceId in dto.ServicesIds)
                {
                    Service? service = await _repManager.ServiceRepository
                        .FindByCondition(s => s.ServiceId == serviceId, false)
                        .SingleOrDefaultAsync() ?? throw new Exception("Service does not exist.");

                    CreationProcess.Stadium.StadiumServices.Clear();
                    CreationProcess.Stadium.StadiumServices.Add(new StadiumService
                    {
                        StadiumId = CreationProcess.Stadium.StadiumId,
                        ServiceId = serviceId,
                    });

                }
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
                var CreationProcess = await _repManager.StadiumCreationProcessRepository.FindByCondition(p => p.Id == processId && p.UserId == userId, false).Include(x => x.Stadium)
                           .FirstOrDefaultAsync();
                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }

                if (dto.EnName != null && dto.ArName != null)
                {
                    var arTranslation = CreationProcess.Stadium.StadiumTranslations.FirstOrDefault(t => t.LanguageId == (long)LanguagesEnum.Arabic);

                    if (arTranslation != null)
                    {
                        arTranslation.Name = dto.ArName;
                    }
                    else
                    {
                        CreationProcess.Stadium.StadiumTranslations.Add(new StadiumTranslation
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Name = dto.ArName,
                        });
                    }
                    var enTranslation = CreationProcess.Stadium.StadiumTranslations.FirstOrDefault(t => t.LanguageId == (long)LanguagesEnum.English);

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
                }
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<long> CreateStadium(DashboardStadiumDto dto, StadiumCreationProcess CreationProcess)
        {
            try
            {
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
                var CreationProcess = await _repManager.StadiumCreationProcessRepository.FindByCondition(p => p.Id == processId && p.UserId == userId, false).Include(x => x.Stadium)
                           .FirstOrDefaultAsync();

                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }
                string coverPhoto = (dto.CoverPhoto == null) ? string.Empty
                    : await Utils.UploadImageFileAsync(dto.CoverPhoto, CreationProcess.Stadium.StadiumId, webHostEnvironment);
                if (!string.IsNullOrEmpty(coverPhoto))
                    CreationProcess.Stadium.Mutimedias.Add(new Mutimedia
                    {
                        StadiumId = CreationProcess.Stadium.StadiumId,
                        FilePath = coverPhoto,
                        IsCover = true,
                        IsVideo = false,
                    });
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
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddStadiumFloorsPrices (List<DashboardStadiumFloorDto> floors, long userId, long processId)
        {
            try
            {
                var CreationProcess = await _repManager.StadiumCreationProcessRepository
                    .FindByCondition(p => p.Id == processId && p.UserId == userId, false)
                    .Include(x => x.Stadium)
                    .ThenInclude(s => s.StadiumFloors)
                           .FirstOrDefaultAsync();

                if (CreationProcess == null) { throw new Exception("Stadium doesn't exist"); }

                var stadiumFloors  = CreationProcess.Stadium.StadiumFloors.ToList();
                if(stadiumFloors.IsNullOrEmpty()) { throw new Exception("Floors don't exist"); }

                floors.ForEach(async f =>
                {
                    var currStadiumFloor = stadiumFloors.Where(sf => sf.StadiumFloorId == f.StadiumFloorId).FirstOrDefault();
                    if (currStadiumFloor == null) { throw new Exception("Stadium Floor don't exist"); }
                    currStadiumFloor.Price = f.Price;
                });
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
