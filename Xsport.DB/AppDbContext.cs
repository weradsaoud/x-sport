using Microsoft.EntityFrameworkCore;
using Xsport.DB.Entities;
namespace Xsport.Db;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<XsportUser> XsportUsers { get; set; }
    public DbSet<Sport> Sports { get; set; }
    public DbSet<Xsport.DB.Entities.Type> Types { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<SportTranslation> SportTranslations { get; set; }
    public DbSet<TypeTranslation> TypeTranslations { get; set; }
    public DbSet<SportPreference> SportPreferences { get; set; }
    public DbSet<SportPreferenceTranslation> SportPreferenceTranslations { get; set; }
    public DbSet<SportPreferenceValue> SportPreferenceValues { get; set; }
    public DbSet<SportPreferenceValueTranslation> SportPreferenceValueTranslations { get; set; }
    public DbSet<UserSportPreference> UserSportPreferences { get; set; }
}