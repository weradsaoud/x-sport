using Microsoft.AspNetCore.Http;
namespace Xsport.DTOs.UserDtos;
public class CompleteRegistrationDto
{
    public List<long>? SportsIds { get; set; }
    public IFormFile File { get; set; } = null!;
}