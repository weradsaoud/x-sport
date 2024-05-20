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
    public class StadiumCreationProcessRepository : RepositoryBase<StadiumCreationProcess>, IStadiumCreationProcessRepository
    {
        public StadiumCreationProcessRepository(AppDbContext db) : base(db)
        {

        }
    }
}
