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
using Xsport.DTOs.CourseDtos.MNGDtos;

namespace Xsport.Core.MNGServices.CourseMNGServices
{
    public class CourseMNGService : ICourseMNGService
    {
        public IRepositoryManager _repManager { get; set; }
        public CourseMNGService(IRepositoryManager repManager)
        {
            _repManager = repManager;
        }

        public async Task<long> CreateCourse([FromBody] CourseDto dto)
        {
            try
            {
                Academy? academy = await _repManager.AcademyRepository
                    .FindByCondition(a => a.AcademyId == dto.AcademyId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Academy Does not exist.");
                Sport? sport = await _repManager.SportRepository
                    .FindByCondition(s => s.SportId == dto.SportId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Sport does not exist.");
                AgeCategory? ageCategory = await _repManager.AgeCategoryRepository
                    .FindByCondition(a => a.AgeCategoryId == dto.AgeCategoryId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Age Category does not exist.");
                Gender gender = await _repManager.GenderRepository
                    .FindByCondition(g => g.GenderId == dto.GenderId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Gender does not exist.");
                Course course = new Course()
                {
                    AcademyId = dto.AcademyId,
                    SportId = dto.SportId,
                    AgeCategoryId = dto.AgeCategoryId,
                    GenderId = dto.GenderId,
                    StartDate = DateOnly.Parse(dto.StartDate),
                    EndDate = DateOnly.Parse(dto.EndDate),
                    Price = dto.Price,
                    CourseTranslations = new List<CourseTranslation>()
                    {
                        new CourseTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Name = dto.ArName,
                            Description = dto.ArDescription,
                        },
                        new CourseTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.English,
                            Name = dto.EnName,
                            Description = dto.EnArDescription
                        }
                    },
                    CourseWorkingDays = dto.WorkingDays.Select(w => new CourseWorkingDay()
                    {
                        WorkingDayId = w.WorkingDayId,
                        StartAt = TimeOnly.Parse(w.StartAt),
                        EndAt = TimeOnly.Parse(w.EndAt)
                    }).ToList(),
                };
                await _repManager.CourseRepository.CreateAsync(course);
                await _repManager.CourseRepository.SaveChangesAsync();
                return course.CourseId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
