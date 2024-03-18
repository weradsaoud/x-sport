using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Xsport.DB.Entities;
namespace Xsport.Db;
public class AppDbContext : IdentityDbContext<XsportUser, XsportRole, long>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //this.Database.CommandTimeout = 180; // Timeout duration in seconds
        this.Database.SetCommandTimeout(TimeSpan.FromSeconds(1000));
    }
    public DbSet<XsportUser> XsportUsers { get; set; }
    public DbSet<XsportRole> XsportRoles { get; set; }
    public DbSet<Sport> Sports { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<SportTranslation> SportTranslations { get; set; }
    public DbSet<XsportRoleTranslation> XsportRoleTranslations { get; set; }
    public DbSet<SportPreference> SportPreferences { get; set; }
    public DbSet<SportPreferenceTranslation> SportPreferenceTranslations { get; set; }
    public DbSet<SportPreferenceValue> SportPreferenceValues { get; set; }
    public DbSet<SportPreferenceValueTranslation> SportPreferenceValueTranslations { get; set; }
    public DbSet<UserSportPreferenceValue> UserSportPreferenceValues { get; set; }
    public DbSet<Level>? Levels { get; set; }
    public DbSet<LevelTranslation> LevelTranslations { get; set; }
    public DbSet<UserSport> UserSports { get; set; }
    public DbSet<Match> Matchs { get; set; }
    public DbSet<UserMatch> UserMatchs { get; set; }
    public DbSet<Academy> Academies { get; set; }
    public DbSet<AcademyTranslation> AcademyTranslations { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseTranslation> CourseTranslations { get; set; }
    public DbSet<AgeCategory> AgeCategories { get; set; }
    public DbSet<AgeCategoryTranslation> AgeCategoryTranslations { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<WorkingDay> WorkingDays { get; set; }
    public DbSet<WorkingDayTranslation> WorkingDayTranslations { get; set; }
    public DbSet<AcademyWorkingDay> AcademyWorkingDays { get; set; }
    public DbSet<CourseWorkingDay> CourseWorkingDays { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceTranslation> ServiceTranslations { get; set; }
    public DbSet<ServiceValue> ServiceValues { get; set; }
    public DbSet<ServiceValueTranslation> ServiceValueTranslations { get; set; }
    public DbSet<AcademyServiceValue> AcademyServiceValues { get; set; }
    public DbSet<Relative> Relatives { get; set; }
    public DbSet<RelativeTranslation> RelativeTranslations { get; set; }
    public DbSet<Mutimedia> Mutimedias { get; set; }
    public DbSet<AcademyReview> AcademyReviews { get; set; }
}