using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB.Entities;
using Xsport.DB.RepositoryInterfaces;

namespace Xsport.DB.Repositories
{
    public class StadiumFloorRepository : RepositoryBase<StadiumFloor>, IStadiumFloorRepository
    {
        public StadiumFloorRepository(AppDbContext db) : base(db)
        {

        }
    }
}
