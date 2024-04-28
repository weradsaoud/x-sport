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

namespace Xsport.Core.ReservationServices
{
    public class ReservationService : IReservationSrvice
    {
        public IRepositoryManager _repManager { get; set; }
        private readonly IHttpContextAccessor httpContextAccessor;
        public ReservationService(IRepositoryManager repManager, IHttpContextAccessor _httpContextAccessor)
        {
            _repManager = repManager;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task<List<SuggestedStadiumDto>> GetSportStadiums(Criteria criteria, short currentLanguageId)
        {
            try
            {
                string domainName = httpContextAccessor.HttpContext?.Request.Scheme
                    + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
                var sportStadiumsQuery = _repManager.StadiumRepository.FindAll(false)
                    .MapStadiumToSuggested(currentLanguageId, domainName, criteria.SportId)
                    .FilterSuggestedStadiums(SuggestedStadiumsFilterOptions.SportId, criteria.SportId.ToString());
                if (!string.IsNullOrEmpty(criteria.StadiumName))
                    sportStadiumsQuery = sportStadiumsQuery.FilterSuggestedStadiums(SuggestedStadiumsFilterOptions.ByStadiumName, criteria.StadiumName);
                if (criteria.Long.HasValue && criteria.Lat.HasValue)
                    return (await sportStadiumsQuery
                        .OrderSuggestedStadiums(SuggestedStadiumsOrderOptions.EvaluationDown)
                        .ToListAsync())
                        .Where(s => Utils.CalculateDistanceBetweenTowUsers(
                                      (decimal)criteria.Lat, (decimal)criteria.Long, s.Lat, s.Long)
                                  <= XsportConstants.SameAreaRaduis).Skip(criteria.PageNum * criteria.PageSize).Take(criteria.PageSize).ToList(); ;

                return await sportStadiumsQuery
                    .OrderSuggestedStadiums(SuggestedStadiumsOrderOptions.EvaluationDown)
                    .Page(criteria.PageNum, criteria.PageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ReservedDay>> GetReservedTimes(long stadiumFloorId, short currentLanguageId)
        {
            try
            {
                StadiumFloor stadiumFloor = await _repManager.StadiumFloorRepository
                    .FindByConditionWithEagerLoad(false,
                    sf => sf.StadiumFloorId == stadiumFloorId,
                    sf => sf.Reservations).SingleOrDefaultAsync() ??
                    throw new Exception("Floor Does not exist.");
                List<Reservation> reservations = stadiumFloor.Reservations
                    .Where(r => r.Date >= DateOnly.FromDateTime(DateTime.UtcNow) &&
                    r.Date <= DateOnly.FromDateTime(DateTime.UtcNow).AddDays(7)).ToList();
                List<ReservedDay> reservedDays = GroupReservationsByDate(reservations, currentLanguageId);
                return reservedDays;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReservationDetails> Reserve(ReserveDto dto, long uId, short currentLangugeId)
        {
            try
            {
                DateOnly reservationDate = DateOnly.Parse(dto.ReservationDate);
                TimeOnly reservatonTimeFrom = TimeOnly.Parse(dto.ReservatonTimeFrom);
                TimeOnly reservatonTimeTo = TimeOnly.Parse(dto.ReservatonTimeTo);
                if (reservationDate < DateOnly.FromDateTime(DateTime.UtcNow))
                    throw new Exception("You can not reserve in the past.");
                if (reservationDate == DateOnly.FromDateTime(DateTime.UtcNow) &&
                    reservatonTimeFrom < TimeOnly.FromDateTime(DateTime.UtcNow) ||
                    reservatonTimeTo < TimeOnly.FromDateTime(DateTime.UtcNow))
                    throw new Exception("You can not reserve in the past.");
                StadiumFloor stadiumFloor = await _repManager.StadiumFloorRepository
                    .FindByConditionWithEagerLoad(false,
                    sf => sf.StadiumFloorId == dto.StadiumFloorId,
                    sf => sf.Reservations,
                    sf => sf.Stadium,
                    sf => sf.Stadium.StadiumTranslations,
                    sf => sf.Floor,
                    sf => sf.Floor.Sport,
                    sf => sf.Floor.Sport.SportTranslations).SingleOrDefaultAsync() ??
                    throw new Exception("Floor Does not exist.");
                var reservationsInDate = stadiumFloor.Reservations.Where(r => r.Date == reservationDate).ToList();
                foreach (var reservation in reservationsInDate)
                {
                    if ((reservatonTimeFrom >= reservation.From && reservatonTimeFrom < reservation.To) ||
                        (reservatonTimeTo > reservation.From && reservatonTimeTo <= reservation.To) ||
                        (reservatonTimeFrom <= reservation.From && reservatonTimeTo >= reservation.To))
                        throw new Exception("Invalid reservation Times.");
                }
                Reservation uReservation = new Reservation()
                {
                    XsportUserId = uId,
                    StadiumFloorId = dto.StadiumFloorId,
                    Date = reservationDate,
                    From = reservatonTimeFrom,
                    To = reservatonTimeTo,
                    Status = 0,
                };
                await _repManager.ReservationRepository.CreateAsync(uReservation);
                await _repManager.ReservationRepository.SaveChangesAsync();
                return new ReservationDetails()
                {
                    StadiumName = stadiumFloor.Stadium.StadiumTranslations
                    .Single(t => t.LanguageId == currentLangugeId).Name,
                    SportName = stadiumFloor.Floor.Sport.SportTranslations
                    .Single(t => t.LanguageId == currentLangugeId).Name,
                    ReservationDate = dto.ReservationDate,
                    ReservationTimeFrom = dto.ReservatonTimeFrom,
                    ReservationTimeTo = dto.ReservatonTimeTo,
                    StadiumRegion = "تضاف لاحقا"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private List<ReservedDay> GroupReservationsByDate(List<Reservation> reservations, short currentLanguageId)
        {
            // Group reservations by date
            var reservationsByDate = reservations.GroupBy(reservation => reservation.Date);

            // Transform each group into a ReservedDay object
            return reservationsByDate.Select(group =>
            {
                var earliestFrom = group.Min(reservation => reservation.From);
                var latestTo = group.Max(reservation => reservation.To);
                var reservedHours = new List<string>();
                for (var time = earliestFrom; time <= latestTo; time = time.AddHours(1))
                {
                    if (group.Any(reservation => time >= reservation.From && time < reservation.To))
                    {
                        reservedHours.Add(time.ToString("HH:00"));
                    }
                }
                string day = group.Key.DayOfWeek.ToString() ?? string.Empty;
                if (currentLanguageId == (short)LanguagesEnum.Arabic)
                    DayOfWeekTranslations.DayOfWeekInArabic.TryGetValue(group.Key.DayOfWeek, out day);
                var reservedDay = new ReservedDay
                {
                    Day = day,
                    Date = group.Key.ToString(XsportConstants.DateOnlyFormat), // Format the date
                    IsWholeDayReserved = reservedHours.Count == 24,
                    ReservedHours = reservedHours
                };
                return reservedDay;
            }).ToList();
        }
    }
}
