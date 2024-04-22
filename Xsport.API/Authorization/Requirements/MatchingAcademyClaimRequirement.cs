using Microsoft.AspNetCore.Authorization;

namespace Xsport.API.Authorization.Requirements
{
    public class MatchingAcademyClaimRequirement : IAuthorizationRequirement
    {
        public MatchingAcademyClaimRequirement(string academyIdClaimType)
        {
            AcademyIdClaimType = academyIdClaimType;
        }
        public string AcademyIdClaimType { get; }
    }
}
