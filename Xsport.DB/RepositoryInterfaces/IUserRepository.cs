using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.DB.Entities;
using Xsport.DTOs.UserDtos;

namespace Xsport.DB.RepositoryInterfaces
{
    public interface IUserRepository : IRepositoryBase<XsportUser>
    {
        public Task<List<PlayersRankingListDto>> GetPlayersRankingList(
            long sportId,
            short currentLanguageId,
            PlayersRankingListOrderOptions orderOption,
            PlayersRankingListFilterOptions filterOption, string filterValue,
            int pageNumZeroStart, int pageSize);


    }
}
