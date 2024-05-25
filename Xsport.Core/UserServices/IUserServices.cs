
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Xsport.Common.Models;
using Xsport.DB.Entities;
using Xsport.DTOs.UserDtos;


namespace Xsport.Core;
public interface IUserServices
{
    public Task<AuthResult> Register(UserRegistrationDto user, short currentLanguageId);
    public Task<short> AccountStatus(XsportUser user);
    //public Task<ConfirmUserEmailRespDto> ConfirmUserEmail(ConfirmEmailDto dto, short currentLanguageId);
    public Task<LoginResponseDto> LoginAsync(UserLoginRequest user, short currentLanguageId);
    public Task<LoginResponseDto> GoogleLoginAsync(UserGoogleLoginDto dto, short currentLanguageId);
    public Task<UserProfileDto> CompleteRegistration(long uId, CompleteRegistrationDto dto, short currentLanguageId);
    public Task<UserProfileDto> GetUserProfile(long uId, short currentLanguageId);
    public Task<UserProfileDto> EditUserProfile(long uId, EditUserReqDto dto, short currentLanguageId);
    public Task<bool> ChangePassword(XsportUser user, ChangePasswordDto dto);
    public Task<bool> ChangeEmail(XsportUser user, ChangeEmailDto dto, short currentLanguageId);
    public Task<UserProfileDto> AddFavoriteSports(AddFavoriteSportReqDto dto, long userId, short currentLanguageId);
    public Task<UserProfileDto> DeleteFavoriteSports(AddFavoriteSportReqDto dto, long userId, short currentLanguageId);
    public Task<UserProfileDto> EditPreferences(EditPreferencesReqDto dto, long userId, short currentLanguageId);
    //public Task<bool> ResendEmailConfirmationCode(UserLoginRequest dto);
    public Task<UserProfileDto> SelectCurrentSport(long uId, long sportId, short currentLanguageId);
    public Task<bool> DeleteAccount(long uId);
    public Task<List<PlayersRankingListDto>> GetPlayers(long uId, GetPlayersReqDto dto, short currentLanguageId);
    public Task<bool> InrollUserInCourse(InrollUserInCourseDto dto);
    public Task<bool> AddAcademyReview(long uId, AddAcademyReviewDto dto);
    public string GenerateEmailConfirmationCode();
    public Task<AuthResult> GenerateJwtToken(XsportUser user, bool enableTwoFactor);
    public Task<UserProfileDto> UploadProfilePicture(long uId, UploadProfilePictureDto dto, short currentLanguageId);
    public Task<UserProfileDto> SkipProfilePicture(long uId, short currentLanguageId);
    public Task<ResetPassworTokendDto> ForgotPassword(ForgotPasswordDto dto, short currentLanuageId);
    public Task<bool> ResetPassword(ResetPasswordDto dto, short currentLanuageId);
}