
using Xsport.DTOs.UserDtos;

namespace Xsport.Core;
public interface IUserServices
{
    public Task<List<SportDto>> Register(UserRegistrationDto user, string Uid, short currentLanguageId);
    public Task<UserProfileDto> CompleteRegistration(CompleteRegistrationDto dto, string Uid, short currentLanguageId);
}