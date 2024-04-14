using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DTOs.GenderDtos.MNGDtos;

namespace Xsport.Core.MNGServices.GenderMNGServices
{
    public class GenderMNGService : IGenderMNGService
    {
        private IRepositoryManager _repManager { get; set; }
        public GenderMNGService(IRepositoryManager repManager)
        {
            _repManager = repManager;
        }
        public async Task<long> CreateGender(GenderDto dto)
        {
            try
            {
                Gender gender = new Gender()
                {
                    GenderTranslations = new List<GenderTranslation>()
                {
                    new GenderTranslation()
                    {
                        LanguageId = (long) LanguagesEnum.English,
                        Name = dto.EnName,
                    },
                    new GenderTranslation()
                    {
                        LanguageId = (long) LanguagesEnum.Arabic,
                        Name = dto.ArName,
                    }
                }
                };
                await _repManager.GenderRepository.CreateAsync(gender);
                await _repManager.GenderRepository.SaveChangesAsync();
                return gender.GenderId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<GenderDto>> GetGenders(short currentLanguageId)
        {
            try
            {
                return await _repManager.GenderRepository.FindAll(false).Select(g => new GenderDto()
                {
                    Id = g.GenderId,
                    ArName = g.GenderTranslations.Single(t => t.LanguageId == (long)LanguagesEnum.Arabic).Name,
                    EnName = g.GenderTranslations.Single(t => t.LanguageId == (long)LanguagesEnum.English).Name
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
