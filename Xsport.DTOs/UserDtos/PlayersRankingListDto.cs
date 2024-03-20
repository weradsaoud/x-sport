namespace Xsport.DTOs.UserDtos
{
    public class PlayersRankingListDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public int Points { get; set; }
        public string Level { get; set; } = null!;
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public string ProfileImgUrl { get; set; } = null!;
    }
}
