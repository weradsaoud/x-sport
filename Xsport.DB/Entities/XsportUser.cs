using Microsoft.AspNetCore.Identity;

namespace Xsport.DB.Entities;
public class XsportUser : IdentityUser<long>
{
    //public long XsportUserId { get; set; }
    public string? Uid { get; set; }
    public string? XsportName { get; set; }
    public string AuthenticationProvider { get; set; } = null!;
    public string? NewEmail { get; set; } = null!;
    //public string? Email { get; set; }
    //public string? Phone { get; set; }
    public string? EmailConfirmationCode { get; set; }
    public int LoyaltyPoints { get; set; }
    public string? Gender { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
    public string? ImagePath { get; set; }

    //public ICollection<UserRole> UserRoles { get; set; } = null!;
    public ICollection<UserSport> UserSports { get; set; } = null!;
    public ICollection<UserMatch> UserMatchs { get; set; } = null!;
    public ICollection<UserCourse> UserCourses { get; set; } = null!;
    public ICollection<AcademyReview> AcademyReviews { get; set; } = null!;
    public ICollection<StadiumReview> StadiumReviews { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = null!;
}