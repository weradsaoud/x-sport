using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Db;
using Xsport.DB.Repositories;

namespace Xsport.DB
{
    public class RepositoryManager : IRepositoryManager, IDisposable
    {
        private AppDbContext _db;
        public RepositoryManager(AppDbContext db)
        {
            _db = db;
        }
        public AppDbContext db
        {
            get
            {
                return _db;
            }
        }
        IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(db);
                return _userRepository;
            }
        }

        public void Dispose() => _db.Dispose();
    }
}
