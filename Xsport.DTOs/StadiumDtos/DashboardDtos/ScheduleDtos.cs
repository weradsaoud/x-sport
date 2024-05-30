using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.StadiumDtos.DashboardDtos
{
    public class OwnerStadiumDto
    {
        public List<long> StadiumsIds { get; set; } = new List<long>();
    }

    public class StadiumFloorsDto
    {
        public List<long> StadiumFloorsIds { get; set; } = new List<long>();
    }

    public class MonthlyReservationDto
    {
       public long StadiumId {  get; set; }
        public int year {  get; set; }
       public int Month {  get; set; }
    }

    public class DailyReservationDto
    {
        public long StadiumFloorId { get; set; }
        public string Date { get; set; }
    }

    public class DashboardDailyReservationSlotDto
    {
        public DateOnly Date { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public long StadiumFloorId { get; set; }
        public ReserverDto Reserver { get; set; }
    }

    public class DashboardDailyReservationDto
    {
        public DateOnly Date { get; set; }
        public TimeOnly From { get; set; }
        public TimeOnly To { get; set; }
        public long StadiumFloorId { get; set; }
        public ReserverDto Reserver { get; set; }
    }
    public class ReserverDto
    {
        public long Id { get; set; }
        public string? XsportName { get; set; }
        public string? ImagePath { get; set; }
    }
    public class DashboardMonthlyReservationDto
    {
        public DateOnly Date { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public List<long> StadiumFloorIds { get; set; } = new List<long>();
        public List<ReserverDto> Reservers { get; set; }
    }
}
