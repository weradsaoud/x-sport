
using Xsport.DTOs.UserDtos;

namespace Xsport.Core;
public interface IUserServices
{
    public Task<RegisterResponseDto> Register(UserRegistrationDto user, short currentLanguageId);
    public Task<LoginResponseDto> LoginAsync(UserLoginRequest user, short currentLanguageId);
    public Task<UserProfileDto> CompleteRegistration(CompleteRegistrationDto dto, long uId, short currentLanguageId);
    public Task<UserProfileDto> GetUserProfile(long uId, short currentLanguageId);
}