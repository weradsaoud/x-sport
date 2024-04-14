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
    public class AcademyWorkingDayRepository
        : RepositoryBase<AcademyWorkingDay>, IAcademyWorkingDayRepository
    {
        public AcademyWorkingDayRepository(AppDbContext db) : base(db)
        {

        }
    }
}
