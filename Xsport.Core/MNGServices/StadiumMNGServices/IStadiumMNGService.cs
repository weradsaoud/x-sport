using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB.Entities;
using Xsport.DTOs.StadiumDtos.MNGDtos;

namespace Xsport.Core.MNGServices.StadiumMNGServices
{
    public interface IStadiumMNGService
    {
        public Task<long> CreateStadium(StadiumDto dto);
        public Task<bool> AddStadiumMultimedia(StadiumMultimediaDto dto);
        public Task<bool> AddStadiumServices(StadiumServicesDto dto);
    }
}
