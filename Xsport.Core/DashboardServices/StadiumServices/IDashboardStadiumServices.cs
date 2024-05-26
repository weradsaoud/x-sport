using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.Common.Utils;
using Xsport.DB.Entities;
using Xsport.DTOs.StadiumDtos.DashboardDtos;
using Xsport.DTOs.StadiumDtos.MNGDtos;

namespace Xsport.Core.DashboardServices.StadiumServices
{
    public interface IDashboardStadiumServices
    {
        //Task<StadiumStepDataModel> SaveStep(long userId, StadiumStepDataModel model);
        Task<List<StadiumProcessCreationDto>> GetStadiumCreatopnProcesses(long userId);
        Task<bool> CompleteCreationProcess(long userId, long processId);
        Task<bool> AddStadiumDecription(DashboardStadiumDiscriptionDto dto, long userId, long processId);
        Task<bool> AddStadiumLocation(DashboardStadiumLocationDto dto, long userId, long processId);
        Task<List<FloorDto>> GetSportsFloors(long sportId);
        Task<List<ServiceDto>> GetServices(short currentLanguageId);
        Task<bool> AddStadiumFloors(DashboardStadiumFloorsDto dto, long userId, long processId);
        Task<bool> AddStadiumServices(DashboardStadiumServicesDto dto, long userId, long processId);
        Task<bool> AddStadiumName(DashboardStadiumNameDto dto, long userId, long processId);
        Task<long> CreateStadium(DashboardStadiumDto dto, long userId, long processId);
        Task<bool> AddStadiumMultimedia(DashboardStadiumMultimediaDto dto, long userId, long processId);
        Task<bool> AddStadiumFloorsPrices(List<DashboardStadiumFloorPriceDto> floors, long userId, long processId);
        Task<bool> AddStadiumWorkingDays(DashboardStadiumWorkingDaysDto dto, long userId, long processId);
        Task<bool> AddPaymentInfo([FromBody] DashboardPaymentInfoDto dto, long userId, long processId);
        Task<bool> AddReservationType(long reservationType, long userId, long processId);
    }
}
