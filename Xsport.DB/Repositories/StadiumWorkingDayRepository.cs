using Xsport.DB.Entities;
using Xsport.DB.RepositoryInterfaces;

namespace Xsport.DB.Repositories
{
    public class StadiumWorkingDayRepository : RepositoryBase<StadiumWorkingDay>, IStadiumWorkingDayRepositpry
    {
        public StadiumWorkingDayRepository(AppDbContext db) : base(db)
        {
            
        }
    }
}
