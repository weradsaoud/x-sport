using Xsport.Common.Enums;
using Xsport.DB.Entities;
using Xsport.DTOs.UserDtos;


namespace Xsport.DB.QueryObjects
{
    public static class PlayersRankingListQueryObject
    {
        public static IQueryable<PlayersRankingListDto> MapXsportUsersToPlayersRankingListDto(
            this IQueryable<XsportUser> users, long sportId, short currentLanguageId)
        {
            try
            {
                return users.Select(u => new PlayersRankingListDto()
                {
                    Id = u.Id,
                    Name = u.XsportName ?? string.Empty,
                    Points = u.UserSports.Single(us => us.SportId == sportId).Points,
                    Level = u.UserSports.Single(us => us.SportId == sportId).Sport
                    .Levels.OrderBy(l => l.MaxPoints)
                    .First(l => u.UserSports.Single(us => us.SportId == sportId).Points <= l.MaxPoints)
                    .LevelTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                    Lat = u.Latitude ?? 0,
                    Long = u.Longitude ?? 0,
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static IQueryable<PlayersRankingListDto> OrderPlayersRankingList(
                this IQueryable<PlayersRankingListDto> players,
                PlayersRankingListOrderOptions orderOption)
        {
            switch (orderOption)
            {
                case PlayersRankingListOrderOptions.None:
                    return players;
                case PlayersRankingListOrderOptions.SimpleOrder:
                    return players.OrderBy(p => p.Id);
                case PlayersRankingListOrderOptions.ByPointsDes:
                    return players.OrderByDescending(p => p.Points);
                case PlayersRankingListOrderOptions.ByPointsAsc:
                    return players.OrderBy(p => p.Points);
                default:
                    throw new ArgumentOutOfRangeException(
                    nameof(orderOption), orderOption, null);
            }
        }

        public static IQueryable<PlayersRankingListDto> FilterPlayersRankingList(
                this IQueryable<PlayersRankingListDto> players,
                PlayersRankingListFilterOptions filterOption, string filterValue)
        {
            if (filterValue == string.Empty) return players;
            switch (filterOption)
            {
                case PlayersRankingListFilterOptions.ByPlayerName:
                    return players.Where(p => p.Name.Contains(filterValue));
                case PlayersRankingListFilterOptions.ByPointsUp:
                    var valueUp = int.Parse(filterValue);
                    return players.Where(p => p.Points >= valueUp);
                case PlayersRankingListFilterOptions.ByPointsDown:
                    var valueDown = int.Parse(filterValue);
                    return players.Where(p => p.Points <= valueDown);
                default:
                    throw new ArgumentOutOfRangeException
                    (nameof(filterOption), filterOption, null);
            }
        }

        //public static IQueryable<PlayersRankingListDto> GetPlayersRankingList(
        //    this IQueryable<XsportUser> users,
        //    long sportId,
        //    short currentLanguageId,
        //    PlayersRankingListOrderOptions orderOption,
        //    PlayersRankingListFilterOptions filterOption, string filterValue,
        //    int pageNumZeroStart, int pageSize)
        //{

        //}
    }
}
