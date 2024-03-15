using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Db;
using Xsport.DB.Repositories;

namespace Xsport.DB
{
    public interface IRepositoryManager
    {
        AppDbContext db { get; }
        IUserRepository UserRepository { get; }
    }
}
