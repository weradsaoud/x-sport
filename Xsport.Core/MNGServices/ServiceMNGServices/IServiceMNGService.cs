using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.ServiceDtos.MNGDtos;

namespace Xsport.Core.MNGServices.ServiceMNGServices
{
    public interface IServiceMNGService
    {
        public Task<long> CreateService(PostServiceDto dto);
        public Task<List<GetServiceDto>> GetServices();
    }
}
