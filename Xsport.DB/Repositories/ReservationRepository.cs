using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB.Entities;
using Xsport.DB.RepositoryInterfaces;

namespace Xsport.DB.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(AppDbContext db) : base(db)
        {

        }
    }
}
