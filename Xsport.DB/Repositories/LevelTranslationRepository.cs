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
    public class LevelTranslationRepository
        : RepositoryBase<LevelTranslation>, ILevelTranslationRepository
    {
        public LevelTranslationRepository(AppDbContext db) : base(db)
        {

        }
    }
}
