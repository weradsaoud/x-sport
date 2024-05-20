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
        public DashboardStadiumDto GenerealInfo { get; set; }
        public List<DashboardStadiumFloorDto> Floors { get; set; }
        public DashboardStadiumMultimediaDto Multimedia { get; set; }
        public DashboardStadiumServicesDto Services { get; set; }
        public DashboardStaduimWorkingDaysDto WorkingDays { get; set; }

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
        [Required] public int Id { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public long FloorId { get; set; }
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
        public long WorkingDayId { get; set; }
        public string OpenAt { get; set; } = null!;
        public string CloseAt { get; set; } = null!;
    }
}
