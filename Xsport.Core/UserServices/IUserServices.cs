
using Microsoft.AspNetCore.Mvc;
using Xsport.DTOs.UserDtos;

namespace Xsport.Core;
public interface IUserServices
{
    public Task<bool> Register(UserRegistrationDto user, short currentLanguageId);
    public Task<ConfirmUserEmailRespDto> ConfirmUserEmail(ConfirmEmailDto dto, short currentLanguageId);
    public Task<LoginResponseDto> LoginAsync(UserLoginRequest user, short currentLanguageId);
    public Task<LoginResponseDto> GoogleLoginAsync(UserGoogleLoginDto dto, short currentLanguageId);
    public Task<UserProfileDto> CompleteRegistration(long uId, CompleteRegistrationDto dto, short currentLanguageId);
    public Task<UserProfileDto> GetUserProfile(long uId, short currentLanguageId);
    public Task<UserProfileDto> EditUserProfile(long uId, EditUserReqDto dto, short currentLanguageId);
    public Task<UserProfileDto> AddFavoriteSports(AddFavoriteSportReqDto dto, long userId, short currentLanguageId);
    public Task<UserProfileDto> DeleteFavoriteSports(AddFavoriteSportReqDto dto, long userId, short currentLanguageId);
    public Task<UserProfileDto> EditPreferences(EditPreferencesReqDto dto, long userId, short currentLanguageId);
    public Task<bool> ResendEmailConfirmationCode(UserLoginRequest dto);
    public Task<UserProfileDto> SelectCurrentSport(long uId, long sportId, short currentLanguageId);
    public Task<bool> DeleteAccount(long uId);
    public Task<List<PlayersRankingListDto>> GetPlayers(long uId, GetPlayersReqDto dto, short currentLanguageId);
}