using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB;
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
        IStadiumRepository _stadiumRepository;
        IMutimediaRepository _mutimediaRepository;
        IServiceRepository _serviceRepository;
        IStadiumServiceRepository _stadiumServiceRepository;
        IAcademyServiceRepository _academyServiceRepository;
        IAcademyWorkingDayRepository _academyWorkingDayRepository;
        IWorkingDayRepository _workingDayRepository;
        IAgeCategoryRepository _ageCategoryRepository;
        ICourseRepository _courseRepository;
        IXsportUserRepository _xsportUserRepository;
        IAcademyReviewRepository _academyReviewRepository;
        IFloorRepository _floorRepository;
        IRelativeRepository _relativeRepository;
        IUserCourseRepository _userCourseRepository;
        IGenderRepository _genderRepository;
        IStadiumFloorRepository _stadiumFloorRepository;
        IReservationRepository _reservationRepository;
        IStadiumWorkingDayRepositpry _stadiumWorkingDayRepositpry;
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
                _sportRepository ??= new SportRepository(db);
                return _sportRepository;
            }
        }
        public IStadiumRepository StadiumRepository
        {
            get
            {
                _stadiumRepository ??= new StadiumRepository(db);
                return _stadiumRepository;
            }
        }
        public IMutimediaRepository MutimediaRepository
        {
            get
            {
                _mutimediaRepository ??= new MutimediaRepository(db);
                return _mutimediaRepository;
            }
        }
        public IServiceRepository ServiceRepository
        {
            get
            {
                _serviceRepository ??= new ServiceRepository(db);
                return _serviceRepository;
            }
        }
        public IStadiumServiceRepository StadiumServiceRepository
        {
            get
            {
                _stadiumServiceRepository ??= new StadiumServiceRepository(db);
                return _stadiumServiceRepository;
            }
        }
        public IAcademyServiceRepository AcademyServiceRepository
        {
            get
            {
                _academyServiceRepository ??= new AcademyServiceRepository(db);
                return _academyServiceRepository;
            }
        }
        public IAcademyWorkingDayRepository AcademyWorkingDayRepository
        {
            get
            {
                _academyWorkingDayRepository ??= new AcademyWorkingDayRepository(db);
                return _academyWorkingDayRepository;
            }
        }
        public IWorkingDayRepository WorkingDayRepository
        {
            get
            {
                _workingDayRepository ??= new WorkingDayRepository(db);
                return _workingDayRepository;
            }
        }
        public IAgeCategoryRepository AgeCategoryRepository
        {
            get
            {
                _ageCategoryRepository ??= new AgeCategoryRepository(db);
                return _ageCategoryRepository;
            }
        }
        public ICourseRepository CourseRepository
        {
            get
            {
                _courseRepository ??= new CourseRepository(db);
                return _courseRepository;
            }
        }
        public IXsportUserRepository XsportUserRepository
        {
            get
            {
                _xsportUserRepository ??= new XsportUserRepository(db);
                return _xsportUserRepository;
            }
        }
        public IAcademyReviewRepository AcademyReviewRepository
        {
            get
            {
                _academyReviewRepository ??= new AcademyReviewRepository(db);
                return _academyReviewRepository;
            }
        }
        public IFloorRepository FloorRepository
        {
            get
            {
                _floorRepository ??= new FloorRepository(db);
                return _floorRepository;
            }
        }
        public IRelativeRepository RelativeRepository
        {
            get
            {
                _relativeRepository ??= new RelativeRepository(db);
                return _relativeRepository;
            }
        }
        public IUserCourseRepository UserCourseRepository
        {
            get
            {
                _userCourseRepository ??= new UserCourseRepository(db);
                return _userCourseRepository;
            }
        }
        public IGenderRepository GenderRepository
        {
            get
            {
                _genderRepository ??= new GenderRepository(db);
                return _genderRepository;
            }
        }
        public IStadiumFloorRepository StadiumFloorRepository
        {
            get
            {
                _stadiumFloorRepository ??= new StadiumFloorRepository(db);
                return _stadiumFloorRepository;
            }
        }
        public IReservationRepository ReservationRepository
        {
            get
            {
                _reservationRepository ??= new ReservationRepository(db);
                return _reservationRepository;
            }
        }
        public IStadiumWorkingDayRepositpry StadiumWorkingDayRepositpry
        {
            get
            {
                _stadiumWorkingDayRepositpry ??= new StadiumWorkingDayRepository(db);
                return _stadiumWorkingDayRepositpry;
            }
        }
        public void Dispose() => _db.Dispose();
    }
}
