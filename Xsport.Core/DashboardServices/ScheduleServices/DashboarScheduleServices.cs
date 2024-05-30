using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Constants;
using Xsport.Common.Enums;
using Xsport.Common.Models;
using Xsport.Common.Utils;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DB.QueryObjects;
using Xsport.DTOs.ReservationDtos;
using Xsport.DTOs.StadiumDtos;
using Xsport.DTOs.StadiumDtos.DashboardDtos;

namespace Xsport.Core.ReservationServices
{
    public class DashboarScheduleServices : IDashboarScheduleServices
    {
        public IRepositoryManager _repManager { get; set; }
        private readonly IHttpContextAccessor httpContextAccessor;
        public DashboarScheduleServices(IRepositoryManager repManager, IHttpContextAccessor _httpContextAccessor)
        {
            _repManager = repManager;
            httpContextAccessor = _httpContextAccessor;
        }

        public async Task<List<DashboardDailyReservationSlotDto>> GetStadiumFloorDailyReservationsSlots(DailyReservationDto dto, short currentLanguageId)
        {
            var reservations = await _repManager.ReservationRepository
                .FindByCondition(r => r.Date == DateOnly.Parse(dto.Date) && r.StadiumFloorId == dto.StadiumFloorId, false)
                .Include(r => r.User)
                .ToListAsync();


            var reservationDtos = new List<DashboardDailyReservationSlotDto>();

            foreach (var reservation in reservations)
            {
                var currentTime = reservation.From;
                while (currentTime <= reservation.To)
                {
                    reservationDtos.Add(new DashboardDailyReservationSlotDto
                    {
                        Date = reservation.Date,
                        Hour = currentTime.Hour,
                        Minute = currentTime.Minute,
                        StadiumFloorId = reservation.StadiumFloorId,
                        Reserver = new ReserverDto()
                        {
                            Id = reservation.User.Id,
                            ImagePath = reservation.User.ImagePath,
                            XsportName = reservation.User.XsportName,
                        }
                    });

                    currentTime = currentTime.AddMinutes(30);
                }
            }


            var result = reservationDtos
                .GroupBy(r => new { r.Date, r.Hour, r.Minute })
                .Select(g => new DashboardDailyReservationSlotDto
                {
                    Date = g.Key.Date,
                    Hour = g.Key.Hour,
                    Minute = g.Key.Minute,
                    StadiumFloorId = g.First().StadiumFloorId,
                    Reserver = g.First().Reserver
                    
                })
                .ToList();

            return result;
        }

        public async Task<List<DashboardDailyReservationDto>> GetStadiumFloorDailyReservations(DailyReservationDto dto, short currentLanguageId)
        {
            var reservations = await _repManager.ReservationRepository
                .FindByCondition(r => r.Date == DateOnly.Parse(dto.Date) && r.StadiumFloorId == dto.StadiumFloorId, false)
                .Include(r => r.User)
                .ToListAsync();

            var reservationDtos = new List<DashboardDailyReservationDto>();
            DashboardDailyReservationDto? currentReservation = null;

            foreach (var reservation in reservations)
            {
                if (currentReservation == null)
                {
                    currentReservation = new DashboardDailyReservationDto
                    {
                        Date = reservation.Date,
                        From = reservation.From,
                        To = reservation.To,
                        StadiumFloorId = reservation.StadiumFloorId,
                        Reserver = new ReserverDto()
                        {
                            ImagePath = reservation.User.ImagePath,
                            Id = reservation.User.Id,
                            XsportName = reservation.User.XsportName,
                        }
                    };
                }
                else if (currentReservation.To == reservation.From)
                {
                    // Extend the current reservation
                    currentReservation.To = reservation.To;
                }
                else
                {
                    // Add the current reservation to the list and start a new one
                    reservationDtos.Add(currentReservation);
                    currentReservation = new DashboardDailyReservationDto
                    {
                        Date = reservation.Date,
                        From = reservation.From,
                        To = reservation.To,
                        StadiumFloorId = reservation.StadiumFloorId,
                        Reserver = new ReserverDto()
                        {
                            ImagePath = reservation.User.ImagePath,
                            Id = reservation.User.Id,
                            XsportName = reservation.User.XsportName,
                        }
                    };
                }
            }

            // Add the last reservation to the list if it exists
            if (currentReservation != null)
            {
                reservationDtos.Add(currentReservation);
            }

            return reservationDtos;
        }
        public async Task<List<DashboardMonthlyReservationDto>> GetStadiumMonthlyReservations(MonthlyReservationDto dto, short currentLanguageId)
        {
            var reservations = await _repManager.ReservationRepository
                .FindByCondition(r => r.Date.Year == dto.year && r.Date.Month == dto.Month, false)
                .Include( r => r.User)
                .ToListAsync();

            var reservationDtos = new List<DashboardMonthlyReservationDto>();

            foreach (var reservation in reservations)
            {
                // For each reservation, add an entry for each 30-minute increment it covers
                var reserver = reservation.User;
                var currentTime = reservation.From;
                while (currentTime <= reservation.To)
                {
                    var existingDto = reservationDtos
                        .FirstOrDefault(r => r.Date == reservation.Date && r.Hour == currentTime.Hour && r.Minute == currentTime.Minute);

                    if (existingDto == null)
                    {
                        existingDto = new DashboardMonthlyReservationDto
                        {
                            Date = reservation.Date,
                            Hour = currentTime.Hour,
                            Minute = currentTime.Minute,
                            StadiumFloorIds = new List<long> { reservation.StadiumFloorId },
                            Reservers = new List<ReserverDto>
                            {
                                new ReserverDto
                                {
                                    Id = reservation.User.Id,
                                    ImagePath = reservation.User.ImagePath,
                                    XsportName = reservation.User.XsportName
                                }
                            }
                        };
                        reservationDtos.Add(existingDto);
                    }
                    else
                    {
                        if (!existingDto.StadiumFloorIds.Contains(reservation.StadiumFloorId))
                        {
                            existingDto.StadiumFloorIds.Add(reservation.StadiumFloorId);
                            existingDto.Reservers.Add(new ReserverDto
                            {
                                Id = reservation.User.Id,
                                XsportName = reservation.User.XsportName,
                                ImagePath = reservation.User.ImagePath
                            });
                        }
                    }
                    currentTime = currentTime.AddMinutes(30);
                }
            }

            var result = reservationDtos
                .GroupBy(r => new { r.Date, r.Hour, r.Minute })
                .Select(g => new DashboardMonthlyReservationDto
                {
                    Date = g.Key.Date,
                    Hour = g.Key.Hour,
                    Minute = g.Key.Minute,
                    StadiumFloorIds = g.SelectMany(x => x.StadiumFloorIds).Distinct().ToList(),
                    Reservers = g.SelectMany(g => g.Reservers).DistinctBy(u => u.Id).ToList()  
                })
                .ToList();

            return result;
        }

    public async Task<OwnerStadiumDto> GetOwnerStadiums(long ownerId)
    {
        var stadiumsDto = new OwnerStadiumDto();
        var ownerStadiums = await _repManager.StadiumRepository.FindByCondition(s => s.OwnerId == ownerId, false).ToListAsync();

        foreach (var item in ownerStadiums)
        {
            stadiumsDto.StadiumsIds.Add(item.StadiumId);
        }
        return stadiumsDto;
    }

    public async Task<StadiumFloorsDto> GetStadiumFloors(long stadiumId)
    {
        var stadiumFloorsDto = new StadiumFloorsDto();
        var StadiumFloors = await _repManager.StadiumFloorRepository.FindByCondition(s => s.StadiumId == stadiumId, false).ToListAsync();

        foreach (var item in StadiumFloors)
        {
            stadiumFloorsDto.StadiumFloorsIds.Add(item.StadiumFloorId);
        }
        return stadiumFloorsDto;
    }
}
}
