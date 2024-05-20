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

namespace Xsport.Core.DashboardServices.StadiumServices
{
    public class DashboardStadiumServices : IDashboardStadiumServices
    {
        public IRepositoryManager _repManager { get; set; }
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IStadiumMNGService _stadiumMNGService;
        public DashboardStadiumServices(
            IRepositoryManager repManager,
            IStadiumMNGService stadiumMNGService,
            IWebHostEnvironment _webHostEnvironment)
        {
            _repManager = repManager;
            _stadiumMNGService = stadiumMNGService;
            webHostEnvironment = _webHostEnvironment;
        }
        public async Task<long> AddStadiumStepOne(StadiumStepOneDto step1DTO)
        {
            long stadiumId = await _stadiumMNGService.CreateStadium(step1DTO.GeneralInfo);
            step1DTO.Floors.ForEach(async floor => await _stadiumMNGService.AddStadiumFloor(floor));
            step1DTO.Services?.ForEach(async service => await _stadiumMNGService.AddStadiumServices(service));

            return stadiumId;
        }
        public async Task<List<StadiumCreationProcess>> StartProcess(string userId)
        {
            try
            {
                var existingProcess = _repManager.StadiumCreationProcessRepository
                        .FindByCondition(p => p.UserId == userId && p.CurrentStep < 5, false).ToList();

                if (existingProcess != null)
                {
                    return existingProcess;
                }

                var newProcess = new StadiumCreationProcess
                {
                    UserId = userId,
                    CurrentStep = 0,
                    LastUpdated = DateTime.UtcNow
                };

                _repManager.StadiumCreationProcessRepository.Create(newProcess);
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return new List<StadiumCreationProcess>() { newProcess };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<StadiumCreationProcess> SaveStep(StadiumStepDataModel model)
        {
            try
            {
                var process = await _repManager.StadiumCreationProcessRepository.FindByCondition(p => p.Id == model.ProcessId && p.UserId == model.UserId,false)
                        .FirstOrDefaultAsync();

                if (process == null)
                {
                    throw new Exception("You are not signed in.");
                }
                await CreateStadium(model.StadiumData.GenerealInfo, process);
                await AddStadiumMultimedia(model.StadiumData.Multimedia, process);
                await AddStadiumServices(model.StadiumData.Services, process);
                model.StadiumData.Floors.ForEach(async f => await AddStadiumFloor(f, process));

                process.CurrentStep = model.CurrentStep;
                process.LastUpdated = DateTime.UtcNow;

                _repManager.StadiumCreationProcessRepository.Update(process);
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();

                return process;
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

        public async Task<long> CreateStadium(DashboardStadiumDto dto, StadiumCreationProcess CreationProcess)
        {
            try
            {

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
                    Latitude = dto.Lat,
                    Longitude = dto.Long,
                    //Price = dto.Price
                };
                CreationProcess.StadiumData = stadium;
                await _repManager.StadiumCreationProcessRepository.SaveChangesAsync();
                return stadium.StadiumId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddStadiumMultimedia(DashboardStadiumMultimediaDto dto, StadiumCreationProcess CreationProcess)
        {
            try
            {
                string coverPhoto = (dto.CoverPhoto == null) ? string.Empty
                    : await Utils.UploadImageFileAsync(dto.CoverPhoto, CreationProcess.StadiumData.StadiumId, webHostEnvironment);
                if (!string.IsNullOrEmpty(coverPhoto))
                    CreationProcess.StadiumData.Mutimedias.Add(new Mutimedia
                    {
                        StadiumId = CreationProcess.StadiumData.StadiumId,
                        FilePath = coverPhoto,
                        IsCover = true,
                        IsVideo = false,
                    });
                string coverVideo = (dto.CoverVideo == null) ? string.Empty
                    : await Utils.UploadImageFileAsync(dto.CoverVideo, CreationProcess.StadiumData.StadiumId, webHostEnvironment);
                if (!string.IsNullOrEmpty(coverVideo))
                    CreationProcess.StadiumData.Mutimedias.Add(new Mutimedia
                    {
                        StadiumId = CreationProcess.StadiumData.StadiumId,
                        FilePath = coverVideo,
                        IsCover = true,
                        IsVideo = true,
                    });
                if (dto.Photos != null)
                    foreach (var photo in dto.Photos)
                    {
                        string photoPath = (photo == null) ? string.Empty
                            : await Utils.UploadImageFileAsync(photo, CreationProcess.StadiumData.StadiumId, webHostEnvironment);
                        if (!string.IsNullOrEmpty(photoPath))
                            CreationProcess.StadiumData.Mutimedias.Add(new Mutimedia
                            {
                                StadiumId = CreationProcess.StadiumData.StadiumId,
                                FilePath = photoPath,
                                IsCover = false,
                                IsVideo = false,
                            });
                    }
                if (dto.Videos != null)
                    foreach (var video in dto.Videos)
                    {
                        string videoPath = (video == null) ? string.Empty
                            : await Utils.UploadImageFileAsync(video, CreationProcess.StadiumData.StadiumId, webHostEnvironment);
                        if (!string.IsNullOrEmpty(videoPath))
                            CreationProcess.StadiumData.Mutimedias.Add(new Mutimedia
                            {
                                StadiumId = CreationProcess.StadiumData.StadiumId,
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
        public async Task<bool> AddStadiumServices(DashboardStadiumServicesDto dto, StadiumCreationProcess CreationProcess)
        {
            try
            {

                foreach (var serviceId in dto.ServicesIds)
                {
                    Service? service = await _repManager.ServiceRepository
                        .FindByCondition(s => s.ServiceId == serviceId, false)
                        .SingleOrDefaultAsync() ?? throw new Exception("Service does not exist.");
                    CreationProcess.StadiumData.StadiumServices.Add(new StadiumService
                    {
                        StadiumId = CreationProcess.StadiumData.StadiumId,
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
        public async Task<bool> AddStadiumFloor(DashboardStadiumFloorDto dto, StadiumCreationProcess CreationProcess)
        {
            try
            {

                Floor? floor = await _repManager.FloorRepository
                    .FindByCondition(f => f.FloorId == dto.FloorId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Stadium does not exist.");
                StadiumFloor stadiumFloor = new StadiumFloor()
                {
                    StadiumId = CreationProcess.StadiumData.StadiumId,
                    FloorId = floor.FloorId,
                    Price = dto.Price

                };
                CreationProcess.StadiumData.StadiumFloors.Add(stadiumFloor);
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
