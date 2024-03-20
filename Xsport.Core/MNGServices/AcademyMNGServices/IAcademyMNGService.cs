using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.AcademyDtos.MNGDtos;

namespace Xsport.Core.MNGServices.AcademyMNGServices
{
    public interface IAcademyMNGService
    {
        public Task<long> CreateAcademy(PostAcademyDto dto);
        public Task<bool> AddAcademyMultimedia(AcademyMultimediaDto dto);
        public Task<bool> AddAcademyServices(AcademyServicesDto dto);
        public Task<bool> AddAcademyWorkingDays(AcademyWorkingDaysDto dto);
    }
}
