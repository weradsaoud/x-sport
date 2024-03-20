using Microsoft.EntityFrameworkCore;
using Xsport.Common.Constants;
using Xsport.Common.Enums;
using Xsport.Common.Utils;
using Xsport.Db;
using Xsport.DB.Entities;
using Xsport.DB.QueryObjects;
using Xsport.DB.RepositoryInterfaces;
using Xsport.DTOs.UserDtos;

namespace Xsport.DB.Repositories
{
    public class UserRepository : RepositoryBase<XsportUser>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db) { }

        public async Task<List<PlayersRankingListDto>> GetPlayersRankingList(
            long sportId,
            short currentLanguageId,
            PlayersRankingListOrderOptions orderOption,
            PlayersRankingListFilterOptions filterOption, string filterValue,
            int pageNumZeroStart, int pageSize, string domainName)
        {
            try
            {
                return await _db.XsportUsers
                    .GetPlayersWithSameFavoriteSport(sportId, false)
                    //.GetPlayersWithSameAreaAsUser(userLat, userLong, false) // ERR: Custom method can not be evaluated by EF
                    .MapXsportUsersToPlayersRankingListDto(sportId, currentLanguageId, domainName)
                    .OrderPlayersRankingList(orderOption)
                    .FilterPlayersRankingList(filterOption, filterValue)
                    .Page<PlayersRankingListDto>(pageNumZeroStart, pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
