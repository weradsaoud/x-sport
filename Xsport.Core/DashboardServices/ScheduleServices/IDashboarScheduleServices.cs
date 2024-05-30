using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.ReservationDtos;
using Xsport.DTOs.StadiumDtos;
using Xsport.DTOs.StadiumDtos.DashboardDtos;

namespace Xsport.Core.ReservationServices
{
    public interface IDashboarScheduleServices
    {
        Task<OwnerStadiumDto> GetOwnerStadiums(long ownerId);
        Task<StadiumFloorsDto> GetStadiumFloors(long stadiumId);
        Task<List<DashboardDailyReservationSlotDto>> GetStadiumFloorDailyReservationsSlots(DailyReservationDto dto, short currentLanguageId);
        Task<List<DashboardDailyReservationDto>> GetStadiumFloorDailyReservations(DailyReservationDto dto, short currentLanguageId);
        Task<List<DashboardMonthlyReservationDto>> GetStadiumMonthlyReservations(MonthlyReservationDto dto, short currentLanguageId);
    }
}
