using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DTOs.RelativeDtos.MNGDtos;

namespace Xsport.Core.MNGServices.RelativeMNGServices
{
    public class RelativeMNGService : IRelativeMNGService
    {
        public IRepositoryManager _repManager { get; set; }
        public RelativeMNGService(IRepositoryManager repManager)
        {
            _repManager = repManager;
        }
        public async Task<long> CreateRelative(RelativeDto dto)
        {
            Relative relative = new Relative()
            {
                RelativeTranslations = new List<RelativeTranslation>()
                {
                    new RelativeTranslation()
                    {
                        LanguageId = (long)LanguagesEnum.English,
                        Name = dto.EnName
                    },
                    new RelativeTranslation()
                    {
                        LanguageId = (long)LanguagesEnum.Arabic,
                        Name = dto.ArName
                    }
                }
            };
            await _repManager.RelativeRepository.CreateAsync(relative);
            await _repManager.RelativeRepository.SaveChangesAsync();
            return relative.RelativeId;
        }
        public async Task<List<RelativeDto>> GetRelatives()
        {
            return await _repManager.RelativeRepository.FindAll(false).Select(r => new RelativeDto()
            {
                Id = r.RelativeId,
                ArName = r.RelativeTranslations
                .Single(t => t.LanguageId == (long)LanguagesEnum.Arabic).Name,
                EnName = r.RelativeTranslations
                .Single(t => t.LanguageId == (long)LanguagesEnum.English).Name
            }).ToListAsync();
        }
    }
}
