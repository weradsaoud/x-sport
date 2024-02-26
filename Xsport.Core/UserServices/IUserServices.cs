
using Xsport.DTOs.UserDtos;

namespace Xsport.Core;
public interface IUserServices
{
    public Task<bool> Register(UserRegistrationDto user, short currentLanguageId);
    public Task<bool> ConfirmUserEmail(string userId, string token);
    public Task<LoginResponseDto> LoginAsync(UserLoginRequest user, short currentLanguageId);
    public Task<UserProfileDto> CompleteRegistration(CompleteRegistrationDto dto, long uId, short currentLanguageId);
    public Task<UserProfileDto> GetUserProfile(long uId, short currentLanguageId);
}