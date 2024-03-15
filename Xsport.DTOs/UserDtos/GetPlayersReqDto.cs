using System.ComponentModel.DataAnnotations;
using Xsport.Common.Models;

namespace Xsport.DTOs.UserDtos
{
    public class GetPlayersReqDto
    {
        public string? Name { get; set; } = string.Empty;
        [Required]
        public PagingInfo PageInfo { get; set; } = null!;
    }
}
