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
    public class SportPreferenceValueRepository
        : RepositoryBase<SportPreferenceValue>, ISportPreferenceValueRepository
    {
        public SportPreferenceValueRepository(AppDbContext db) : base(db)
        {

        }
    }
}
