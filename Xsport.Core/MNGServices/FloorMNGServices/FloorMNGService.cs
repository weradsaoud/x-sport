using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DTOs.FloorDtos.MNGDtos;

namespace Xsport.Core.MNGServices.FloorMNGServices
{
    public class FloorMNGService : IFloorMNGService
    {
        public IRepositoryManager _repManager { get; set; }
        public FloorMNGService(IRepositoryManager repManager)
        {
            _repManager = repManager;
        }

        public async Task<long> CreateFloor(FloorDto dto)
        {
            try
            {
                Floor floor = new Floor()
                {
                    FloorTranslations = new List<FloorTranslation>()
                {
                    new FloorTranslation()
                    {
                        LanguageId = (long)LanguagesEnum.Arabic,
                        Name = dto.ArName,
                    },
                    new FloorTranslation()
                    {
                        LanguageId = (long)LanguagesEnum.English,
                        Name = dto.EnName,
                    }
                }
                };
                await _repManager.FloorRepository.CreateAsync(floor);
                await _repManager.FloorRepository.SaveChangesAsync();
                return floor.FloorId;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
