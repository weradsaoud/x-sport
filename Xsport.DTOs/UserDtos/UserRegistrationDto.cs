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
}