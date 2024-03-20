using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.AdminDtos;
using Xsport.DTOs.AgeCategoryDtos.MNGDtos;

namespace Xsport.Core.MNGServices.AgeCategoryMNGServices
{
    public interface IAgeCategoryMNGService
    {
        public Task<long> CreateAgeCategory(AgeCategoryDto dto);
    }
}
