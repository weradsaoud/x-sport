using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Constants;
using Xsport.Common.Enums;
using Xsport.Db;
using Xsport.DB.Entities;
using Xsport.DB.QueryObjects;
using Xsport.DB.RepositoryInterfaces;
using Xsport.DTOs.AcademyDtos;
using Xsport.DTOs.AdminDtos;

namespace Xsport.DB.Repositories
{
    public class AcademyRepository : RepositoryBase<Academy>, IAcademyRepository
    {
        public AcademyRepository(AppDbContext db) : base(db) { }

        public async Task<AcademyReviewDto> GetAcademyReviews(long academyId, short currentLanguageId)
        {
            try
            {
                return await _db.Academies.Where(a => a.AcademyId == academyId).Select(a => new AcademyReviewDto
                {
                    AcademyId = a.AcademyId,
                    AcademyName = a.AcademyTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                    CoverPhoto = a.Mutimedias.Single(m => !m.IsVideo && m.IsCover).FilePath,
                    CoverVideo = a.Mutimedias.Single(m => m.IsVideo && m.IsCover).FilePath,
                    Photos = a.Mutimedias.Where(m => !m.IsVideo && !m.IsCover).Select(p => p.FilePath),
                    Videos = a.Mutimedias.Where(m => m.IsVideo && !m.IsCover).Select(p => p.FilePath),
                    Reviews = a.AcademyReviews.Select(r => new ReviewDto
                    {
                        ReviewId = r.AcademyReviewId,
                        UserId = r.XsportUserId,
                        UserName = r.XsportUser.XsportName ?? string.Empty,
                        UserImg = r.XsportUser.ImagePath ?? string.Empty,
                        ReviewContent = r.Description,
                        Evaluation = r.Evaluation,
                        ReviewDateTime = r.ReviewDateTime.ToString(XsportConstants.DateTimeFormat),
                    })
                }).FirstAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AcademyCoursesDto> GetCoursesInDate(
            long academyId, short currentLanguageId, DateOnly targetDate)
        {
            try
            {
                return await _db.Academies.Where(a => a.AcademyId == academyId)
                .Select(a => new AcademyCoursesDto()
                {
                    AcademyId = a.AcademyId,
                    AcademyName = a.AcademyTranslations
                    .Single(t => t.LanguageId == currentLanguageId).Name,
                    CoverVideo = a.Mutimedias.Single(m => m.IsVideo && m.IsCover).FilePath,
                    CoverPhoto = a.Mutimedias.Single(m => !m.IsVideo && m.IsCover).FilePath,
                    Photos = a.Mutimedias.Where(m => !m.IsVideo && !m.IsCover)
                    .Select(p => p.FilePath),
                    Videos = a.Mutimedias.Where(m => m.IsVideo && !m.IsCover)
                    .Select(p => p.FilePath),
                    Date = DateOnly.FromDateTime(DateTime.Today).ToString(XsportConstants.DateOnlyFormat),
                    AgeCategoriesWithCoursesInDate = a.AgeCategorys
                    .Select(ac => new AgeCategoriesWithCoursesInDate()
                    {
                        AgeCategoryId = ac.AgeCategoryId,
                        FromAge = ac.FromAge,
                        ToAge = ac.ToAge,
                        Courses = ac.Courses.Where(c => c.AcademyId == a.AcademyId)
                        .Where(c => c.StartDate <= targetDate && c.EndDate >= targetDate)
                        .Where(c => c.CourseWorkingDays.Select(w => w.WorkingDay.OrderInWeek).Contains((int)targetDate.DayOfWeek))
                        .Select(c => new CoursesDto()
                        {
                            CourseId = c.CourseId,
                            CourseName = c.CourseTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                            Description = c.CourseTranslations.Single(t => t.LanguageId == currentLanguageId).Description,
                            StartTime = c.StartDate.ToString(XsportConstants.TimeOnlyFormat),
                            EndTime = c.EndDate.ToString(XsportConstants.TimeOnlyFormat),
                            SportId = c.SportId,
                            SportName = c.Sport.SportTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                        })
                    })
                }).FirstAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AboutAcademyDto> GetAboutAcademy(
            long academyId, short currentLanguageId)
        {
            try
            {
                return await _db.Academies.Where(a => a.AcademyId == academyId)
                    .Select(academy => new AboutAcademyDto()
                    {
                        AcademyId = academy.AcademyId,
                        Name = academy.AcademyTranslations
                        .Single(t => t.LanguageId == currentLanguageId).Name,
                        Description = academy.AcademyTranslations
                        .Single(t => t.LanguageId == currentLanguageId).Description,
                        Phone = academy.Phone,
                        Lat = academy.Lattitude,
                        Long = academy.Longitude,
                        OpenAt = academy.OpenAt,
                        CloseAt = academy.CloseAt,
                        MinPrice = academy.Courses.OrderBy(c => c.Price).First().Price,
                        MaxPrice = academy.Courses
                        .OrderByDescending(c => c.Price).First().Price,
                        services = academy.AcademyServiceValues
                        .Select(asv => new ServiceDto()
                        {
                            ServiceId = asv.ServiceValue.ServiceId,
                            ServiceName = asv.ServiceValue.Service
                            .ServiceTranslations
                            .Single(t => t.LanguageId == currentLanguageId).Name,
                            ValueId = asv.ServiceValueId,
                            ValueName = asv.ServiceValue
                            .ServiceValueTranslations
                            .Single(t => t.LanguageId == currentLanguageId).Name
                        })
                    }).SingleAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<SuggestedAcademyDto> GetSuggestedAcademies(short currentLanguageId,
            SuggestedAcademiesOrderOptions orderOption,
            SuggestedAcademiesFilterOptions filterOption, string filterValue,
            int pageNumber, int pageSize)
        {
            try
            {
                return _db.Academies.MapAcademiesToSuggested(currentLanguageId)
                    .OrderSuggestedAcademies(orderOption)
                    .FilterSuggestedAcademies(filterOption, filterValue)
                    .Page<SuggestedAcademyDto>(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SportMemberShipDto> GetUserSportsAcademies(
            long uId, short currentLanguageId)
        {
            try
            {
                var userSportsAcademies = _db.XsportUsers.Single(u => u.Id == uId)
                    .UserCourses.Where(uc => uc.IsPersonal)
                    .Select(uc => new SportMemberShipDto()
                    {
                        AcademyId = uc.Course.AcademyId,
                        AcademyName = uc.Course.Academy.AcademyTranslations
                        .Single(t => t.LanguageId == currentLanguageId).Name,
                        SportId = uc.Course.SportId,
                        SportName = uc.Course.Sport.SportTranslations
                        .Single(t => t.LanguageId == currentLanguageId).Name,
                        UserPoints = uc.Points
                    }).ToList();
                return userSportsAcademies;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AgeCategory> AddAgeCategory(AddAgeCategoryDto dto)
        {
            try
            {
                Academy academy = await _db.Academies.SingleOrDefaultAsync(a => a.AcademyId == dto.AcademyId) ??
                    throw new Exception("Academy does not exist.");
                AgeCategory ageCategory = new AgeCategory()
                {
                    AcademyId = dto.AcademyId,
                    Academy = academy,
                    AgeCategoryTranslations = new List<AgeCategoryTranslation>
                    {
                        new AgeCategoryTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Name = dto.ArName,
                        },
                        new AgeCategoryTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.English,
                            Name = dto.EnName,
                        }
                    },
                    FromAge = dto.FromAge,
                    ToAge = dto.ToAge
                };
                await _db.AddAsync(ageCategory);
                await _db.SaveChangesAsync();
                return ageCategory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Course> AddCourse(AddCourseDto dto)
        {
            try
            {
                Academy academy = await _db.Academies.SingleOrDefaultAsync(a => a.AcademyId == dto.AcademyId) ??
                    throw new Exception("Academy Does not exist.");
                Sport sport = await _db.Sports.SingleOrDefaultAsync(s => s.SportId == dto.SportId) ??
                    throw new Exception("Sport does not exist.");
                AgeCategory ageCategory = await _db.AgeCategories.SingleOrDefaultAsync(a => a.AcademyId == dto.AgeCategoryId) ??
                    throw new Exception("Age Category does not exist.");
                Course course = new Course()
                {
                    AcademyId = dto.AcademyId,
                    SportId = dto.SportId,
                    AgeCategoryId = dto.AcademyId,
                    Academy = academy,
                    Sport = sport,
                    AgeCategory = ageCategory,
                    StartDate = DateOnly.Parse(dto.StartDate),
                    EndDate = DateOnly.Parse(dto.EndDate),
                    Price = dto.Price,
                    CourseTranslations = new List<CourseTranslation>()
                    {
                        new CourseTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Name = dto.ArName
                        },
                        new CourseTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.English,
                            Name = dto.EnName
                        }
                    },
                    CourseWorkingDays = dto.WorkingDays.Select(w => new CourseWorkingDay()
                    {
                        WorkingDayId = w.WorkingDayId,
                        StartAt = TimeOnly.Parse(dto.StartDate),
                        EndAt = TimeOnly.Parse(dto.EndDate)
                    }).ToList(),
                };
                await _db.Courses.AddAsync(course);
                await _db.SaveChangesAsync();
                return course;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> InrollUserInCourse(long uId, long courseId, bool isPersonal)
        {
            try
            {
                XsportUser user = await _db.XsportUsers.SingleOrDefaultAsync(u => u.Id == uId) ??
                    throw new Exception("User does not exist.");
                Course course = await _db.Courses.SingleOrDefaultAsync(_ => _.CourseId == courseId) ??
                    throw new Exception("Course does not exist.");
                UserCourse userCourse = new UserCourse()
                {
                    CourseId = courseId,
                    Course = course,
                    XsportUserId = user.Id,
                    XsportUser = user,
                    Points = 0,
                    IsPersonal = isPersonal
                };
                await _db.UserCourses.AddAsync(userCourse);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddServiceToAcademy(long academyId, long serviceValueId)
        {
            try
            {
                Academy academy = await _db.Academies.SingleOrDefaultAsync(a => a.AcademyId == academyId) ??
                    throw new Exception("Academy does not exist.");
                ServiceValue serviceValue = await _db.ServiceValues
                    .SingleOrDefaultAsync(s => s.ServiceValueId == serviceValueId) ??
                    throw new Exception("The provided service value does not exist.");
                AcademyServiceValue academyServiceValue = new AcademyServiceValue()
                {
                    ServiceValueId = serviceValueId,
                    ServiceValue = serviceValue,
                    AcademyId = academyId,
                    Academy = academy
                };
                await _db.AcademyServiceValues.AddAsync(academyServiceValue);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Academy> AddAcademy(
            AddAcademyDto dto, string coverPhotoPath, string coverVideoPath,
            List<string> photosPathes, List<string> vidoessPathes)
        {
            var multimedias = new List<Mutimedia>()
                {
                    new Mutimedia()
                    {
                        FilePath = coverPhotoPath,
                        IsCover = true,
                        IsVideo = false,
                    },
                    new Mutimedia()
                    {
                        FilePath = coverVideoPath,
                        IsCover = true,
                        IsVideo = true,
                    }
                };
            multimedias.AddRange(photosPathes.Select(p => new Mutimedia()
            {
                FilePath = p,
                IsCover = false,
                IsVideo = false
            }).ToList());
            multimedias.AddRange(vidoessPathes.Select(v => new Mutimedia()
            {
                FilePath = v,
                IsCover = false,
                IsVideo = true
            }));
            Academy academy = new Academy()
            {
                AcademyTranslations = new List<AcademyTranslation>
                {
                    new AcademyTranslation()
                    {
                        LanguageId = (long)LanguagesEnum.Arabic,
                        Name = dto.ArName,
                        Description = dto.ArDescription,
                    },
                    new AcademyTranslation()
                    {
                        LanguageId = (long)LanguagesEnum.English,
                        Name = dto.EnName,
                        Description = dto.EnDescription,
                    }
                },
                Phone = dto.Phone,
                OpenAt = TimeOnly.Parse(dto.OpenAt),
                CloseAt = TimeOnly.Parse(dto.CloseAt),
                Lattitude = dto.Lattitude,
                Longitude = dto.Longitude,
                Mutimedias = multimedias,
            };
            await _db.Academies.AddAsync(academy);
            await _db.SaveChangesAsync();
            return academy;
        }
        public async Task<bool> AddService(AddServiceDto dto)
        {
            try
            {
                Service service = new Service()
                {
                    ServiceTranslations = new List<ServiceTranslation>()
                    {
                        new ServiceTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Name = dto.ArName
                        },
                        new ServiceTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.English,
                            Name = dto.EnName
                        }
                    },
                    ServiceValues = dto.Values.Select(v => new ServiceValue()
                    {
                        ServiceValueTranslations = new List<ServiceValueTranslation>()
                        {
                            new ServiceValueTranslation()
                            {
                                LanguageId = (long)LanguagesEnum.Arabic,
                                Name = v.ArName
                            },
                            new ServiceValueTranslation()
                            {
                                LanguageId = (long)LanguagesEnum.English,
                                Name = v.EnName
                            }
                        },
                    }).ToList()
                };
                await _db.Services.AddAsync(service);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<GetServicesDto>> GetServices(short currentLanguageId)
        {
            try
            {
                return await _db.Services.Select(s => new GetServicesDto()
                {
                    Id = s.ServiceId,
                    Name = s.ServiceTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                    Values = s.ServiceValues.Select(v => new GetServicesValuesDto()
                    {
                        Id = v.ServiceValueId,
                        Name = v.ServiceValueTranslations.Single(t => t.LanguageId == currentLanguageId).Name
                    }).ToList()
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<GetWorkingDayDto>> GetWorkingDays(short currentLanguageId)
        {
            try
            {
                return await _db.WorkingDays.Select(w => new GetWorkingDayDto()
                {
                    Id = w.WorkingDayId,
                    Name = w.WorkingDayTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                    Order = w.OrderInWeek
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
