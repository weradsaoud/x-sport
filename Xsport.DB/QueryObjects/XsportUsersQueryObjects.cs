using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Constants;
using Xsport.Common.Utils;
using Xsport.DB.Entities;

namespace Xsport.DB.QueryObjects
{
    public static class XsportUsersQueryObjects
    {
        public static IQueryable<XsportUser> GetPlayersWithSameAreaAsUser(
            this IQueryable<XsportUser> users,
            decimal userLat, decimal userLong, bool trackChanges)
        {
            return trackChanges ? users.Where(u =>
            Utils.CalculateDistanceBetweenTowUsers(
                userLat, userLong, u.Latitude ?? 0, u.Longitude ?? 0) <= XsportConstants.SameAreaRaduis)
                :
                users.Where(u =>
            Utils.CalculateDistanceBetweenTowUsers(
                userLat, userLong, u.Latitude ?? 0, u.Longitude ?? 0) <= XsportConstants.SameAreaRaduis)
                .AsNoTracking();
        }

        public static IQueryable<XsportUser> GetPlayersWithSameFavoriteSport(
            this IQueryable<XsportUser> users,
            long sportId, bool trackChanges)
        {
            return trackChanges ?
                users.Where(u => u.UserSports.Select(us => us.SportId).Contains(sportId)) :
                users.Where(u => u.UserSports.Select(us => us.SportId).Contains(sportId))
                .AsNoTracking();
        }
    }
}
