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
    public class WorkingDayRepository : RepositoryBase<WorkingDay>, IWorkingDayRepository
    {
        public WorkingDayRepository(AppDbContext db) : base(db)
        {

        }
    }
}
