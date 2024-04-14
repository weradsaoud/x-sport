using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Constants;
using Xsport.Common.Enums;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DB.QueryObjects;
using Xsport.DB.RepositoryInterfaces;
using Xsport.DTOs.AcademyDtos;
using Xsport.DTOs.AdminDtos;

namespace Xsport.DB.Repositories
{
    public class AcademyRepository : RepositoryBase<Academy>, IAcademyRepository
    { 
        public AcademyRepository(AppDbContext db) : base(db)
        {

        }
        public async Task<bool> AddWorkingDays(AddWorkingDaysDto dto)
        {
            try
            {
                if (_db.WorkingDays.Any()) throw new Exception("Working Days are already there.");
                foreach (var day in dto.WorkingDays)
                {
                    WorkingDay workingDay = new WorkingDay()
                    {
                        WorkingDayTranslations = new List<WorkingDayTranslation>()
                    {
                        new WorkingDayTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Name = day.ArName
                        },
                        new WorkingDayTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.English,
                            Name = day.EnName
                        }
                    },
                        OrderInWeek = day.OrderInWeek
                    };
                    await _db.WorkingDays.AddAsync(workingDay);
                    await _db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
