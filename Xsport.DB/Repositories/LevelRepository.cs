using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Db;
using Xsport.DB.Entities;
using Xsport.DB.RepositoryInterfaces;

namespace Xsport.DB.Repositories
{
    public class LevelRepository : RepositoryBase<Level>, ILevelRepository
    {
        public LevelRepository(AppDbContext db) : base(db)
        {

        }
    }
}
