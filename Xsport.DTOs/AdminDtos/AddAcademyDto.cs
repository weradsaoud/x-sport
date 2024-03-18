using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AdminDtos
{
    public class AddAcademyDto
    {
        public string ArName { get; set; } = null!;
        public string EnName { get; set; } = null!;
        public string ArDescription { get; set; } = null!;
        public string EnDescription { get; set; } = null!;
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
        public string Phone { get; set; } = null!;
        public string OpenAt { get; set; } = null!;
        public string CloseAt { get; set; } = null!;
        //public List<AddAgeCategoryDto> AgeCategories { get; set; } = null!;
        //public List<long> AcademyServicesValues { get; set; } = null!;
        //public List<AcademyWorkingDayDto> WorkingDays { get; set; } = null!;
        public IFormFile? CoverPhoto { get; set; } = null!;
        public IFormFile? CoverVideo { get; set; } = null!;
        public List<IFormFile>? Photos { get; set; } = null!;
        public List<IFormFile>? Videos { get; set; } = null!;
    }

    public class AcademyWorkingDayDto
    {
        public long WorkingDayId { get; set; }
        public string OpenAt { get; set; } = null!;
        public string CloseAt { get; set; } = null!;
    }
}
