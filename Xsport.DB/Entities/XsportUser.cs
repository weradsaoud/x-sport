namespace Xsport.DB.Entities;
public class XsportUser
{
    public long XsportUserId { get; set; }
    public string? Uid { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
    public string? ImagePath { get; set; }

    public ICollection<UserType>? UserTypes { get; set; }
}