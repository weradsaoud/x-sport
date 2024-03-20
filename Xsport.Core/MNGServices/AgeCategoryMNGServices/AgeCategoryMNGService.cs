using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DTOs.AdminDtos;
using Xsport.DTOs.AgeCategoryDtos.MNGDtos;

namespace Xsport.Core.MNGServices.AgeCategoryMNGServices
{
    public class AgeCategoryMNGService:IAgeCategoryMNGService
    {
        public IRepositoryManager _repManager { get; set; }
        public AgeCategoryMNGService(IRepositoryManager repManager)
        {
            _repManager = repManager;
        }
        public async Task<long> CreateAgeCategory(AgeCategoryDto dto)
        {
            try
            {
                Academy? academy = await _repManager.AcademyRepository
                    .FindByCondition(a => a.AcademyId == dto.AcademyId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Academy does not exsit.");
                AgeCategory ageCategory = new AgeCategory()
                {
                    AcademyId = dto.AcademyId,
                    AgeCategoryTranslations = new List<AgeCategoryTranslation>()
                    {
                        new AgeCategoryTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Name = dto.ArName
                        },
                        new AgeCategoryTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.English,
                            Name = dto.EnName
                        }
                    },
                    FromAge = dto.FromAge,
                    ToAge = dto.ToAge,
                };
                await _repManager.AgeCategoryRepository.CreateAsync(ageCategory);
                await _repManager.AgeCategoryRepository.SaveChangesAsync();
                return ageCategory.AgeCategoryId;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
