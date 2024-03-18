using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Db;
using Xsport.DB.Entities;

namespace Xsport.DB.Repositories
{
    public class ServiceTranslationRepository : RepositoryBase<ServiceTranslation>
    {
        public ServiceTranslationRepository(AppDbContext db) : base(db)
        {

        }
    }
}
