using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Db;
using Xsport.DB.Repositories;
using Xsport.DB.RepositoryInterfaces;

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
        IAcademyRepository _academyRepository;
        ISportRepository _sportRepository;
        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(db);
                return _userRepository;
            }
        }
        public IAcademyRepository AcademyRepository
        {
            get
            {
                _academyRepository ??= new AcademyRepository(db);
                return _academyRepository;
            }
        }
        public ISportRepository SportRepository 
        { 
            get
            {
                _sportRepository??= new SportRepository(db);
                return _sportRepository;
            }
        }

        public void Dispose() => _db.Dispose();
    }
}
