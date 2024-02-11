namespace Xsport.DTOs.UserDtos;
public class UserRegistrationDto
{
    public string? Name { get; set; }
    public string? Uid { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
}