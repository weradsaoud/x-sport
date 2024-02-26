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
    //public DbSet<UserType> UserTypes { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<SportTranslation> SportTranslations { get; set; }
    public DbSet<XsportRoleTranslation> XsportRoleTranslations { get; set; }
    public DbSet<SportPreference> SportPreferences { get; set; }
    public DbSet<SportPreferenceTranslation> SportPreferenceTranslations { get; set; }
    public DbSet<SportPreferenceValue> SportPreferenceValues { get; set; }
    public DbSet<SportPreferenceValueTranslation> SportPreferenceValueTranslations { get; set; }
    public DbSet<UserSportPreference> UserSportPreferences { get; set; }
    public DbSet<Level>? Levels { get; set; }
    public DbSet<LevelTranslation> LevelTranslations { get; set; }
}