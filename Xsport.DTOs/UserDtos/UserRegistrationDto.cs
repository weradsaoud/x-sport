namespace Xsport.DTOs.UserDtos;
public class UserRegistrationDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Gender { get; set; } = null!;
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
}