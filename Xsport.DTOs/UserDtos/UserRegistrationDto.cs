using System.ComponentModel.DataAnnotations;

namespace Xsport.DTOs.UserDtos;
public class UserRegistrationDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string Phone { get; set; } = null!;
    [Required]
    public string Gender { get; set; } = null!;
    [Required]
    public decimal Longitude { get; set; }
    [Required]
    public decimal Latitude { get; set; }
}