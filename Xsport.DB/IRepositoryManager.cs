using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB.RepositoryInterfaces;

namespace Xsport.DB
{
    public interface IRepositoryManager
    {
        AppDbContext db { get; }
        IUserRepository UserRepository { get; }
        IAcademyRepository AcademyRepository { get; }
        ISportRepository SportRepository { get; }
        IStadiumRepository StadiumRepository { get; }
        IMutimediaRepository MutimediaRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IStadiumServiceRepository StadiumServiceRepository { get; }
        IAcademyServiceRepository AcademyServiceRepository { get; }
        IAcademyWorkingDayRepository AcademyWorkingDayRepository { get; }
        IWorkingDayRepository WorkingDayRepository { get; }
        IAgeCategoryRepository AgeCategoryRepository { get; }
        ICourseRepository CourseRepository { get; }
        IXsportUserRepository XsportUserRepository { get; }
        IAcademyReviewRepository AcademyReviewRepository { get; }
        IFloorRepository FloorRepository { get; }
        IRelativeRepository RelativeRepository { get; }
        IUserCourseRepository UserCourseRepository { get; }
        IGenderRepository GenderRepository { get; }
        IStadiumFloorRepository StadiumFloorRepository { get; }
        IReservationRepository ReservationRepository { get; }
    }
}
