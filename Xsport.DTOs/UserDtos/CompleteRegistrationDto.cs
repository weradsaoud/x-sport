using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace Xsport.DTOs.UserDtos;
public class CompleteRegistrationDto
{
    [Required]
    public List<long> SportsIds { get; set; } = null!;
    public IFormFile? File { get; set; } = null!;
}