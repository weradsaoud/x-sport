using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DB.RepositoryInterfaces;

namespace Xsport.DB.Repositories
{
    public class StadiumRepository : RepositoryBase<Stadium>, IStadiumRepository
    {
        public StadiumRepository(AppDbContext db) : base(db)
        {

        }

        public IQueryable<Stadium> GetBySportId(long sportId)
        {
            return _db.Stadiums.Where(s => s.SportId == sportId);
        }
    }
}
