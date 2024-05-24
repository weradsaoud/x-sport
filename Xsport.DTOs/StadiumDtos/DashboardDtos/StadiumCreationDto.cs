using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.StadiumDtos.DashboardDtos
{
    public class StadiumCreationDto
    {
        public DashboardStadiumDto? GenerealInfo { get; set; }
        public DashboardStadiumFloorsDto? Floors { get; set; }
        public DashboardStadiumMultimediaDto? Multimedia { get; set; }
        public DashboardStadiumServicesDto? Services { get; set; }
        public DashboardStaduimWorkingDaysDto? WorkingDays { get; set; }

    }

    public class StadiumDashboardDto
    {
        public DashboardStadiumDto? GenerealInfo { get; set; }
        public List<DashboardStadiumFloorDto>? Floors { get; set; }
        public string CoverPhoto { get; set; } = null!;
        public string CoverVideo { get; set; } = null!;
        public List<string> Photos { get; set; } = null!;
        public List<string> Videos { get; set; } = null!;
        public DashboardStadiumServicesDto? Services { get; set; }
        public DashboardStaduimWorkingDaysDto? WorkingDays { get; set; }
    }

    public class DashboardStadiumDiscriptionDto
    {
        public string? ArDescription { get; set; } = null!;
        public string? EnDescription { get; set; } = null!;
    }
    public class DashboardStadiumLocationDto
    {
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
    public class DashboardStadiumDto
    {
        public string? ArName { get; set; } = null!;
        public string? EnName { get; set; } = null!;
        public string? ArDescription { get; set; } = null!;
        public string? EnDescription { get; set; } = null!;
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
    }

    public class DashboardStadiumFloorDto
    {
        public long StadiumFloorId { get; set; }
        public decimal Price { get; set; }
    }

    public class DashboardStadiumFloorsDto
    {
        public List<long> FloorIds { get; set; } = null!;
    }
    public class FloorDto
    {
        public long FloorId { get; set; }
        public int NumOfPlayers { get; set; }
    }

    public class StadiumFloorDto
    {
        public long FloorId { get; set; }
        public int NumOfPlayers { get; set; }
    }
    public class DashboardStadiumNameDto
    {
        public string ArName { get; set; } = null!;
        public string EnName { get; set; } = null!;
    }
    public class DashboardStadiumMultimediaDto
    {
        public IFormFile? CoverPhoto { get; set; } = null!;
        public IFormFile? CoverVideo { get; set; } = null!;
        public List<IFormFile>? Photos { get; set; } = null!;
        public List<IFormFile>? Videos { get; set; } = null!;

    }

    public class DashboardStadiumServicesDto
    {
        public List<long> ServicesIds { get; set; } = null!;
    }

    public class DashboardStaduimWorkingDaysDto
    {
        public List<DashboardStadiumWorkingDayDto> StadiumWorkingDays { get; set; } = null!;
    }
    public class DashboardStadiumWorkingDayDto
    {
        public long? WorkingDayId { get; set; }
        public string? OpenAt { get; set; } = null!;
        public string? CloseAt { get; set; } = null!;
    }
}
