using Microsoft.EntityFrameworkCore;
namespace Xsport.Db;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    
}