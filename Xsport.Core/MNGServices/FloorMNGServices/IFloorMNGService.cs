using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.FloorDtos.MNGDtos;

namespace Xsport.Core.MNGServices.FloorMNGServices
{
    public interface IFloorMNGService
    {
        public Task<long> CreateFloor(FloorDto dto);
    }
}
