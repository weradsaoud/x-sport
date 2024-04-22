using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Xsport.API.Authorization.Requirements;

namespace Xsport.API.Authorization.Handlers
{
    public class MatchingAcademyClaimHandler : AuthorizationHandler<MatchingAcademyClaimRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MatchingAcademyClaimHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, MatchingAcademyClaimRequirement requirement)
        {
            var expectedAcademyId = _httpContextAccessor.HttpContext?.GetRouteData()?.Values["academyId"]?.ToString(); // Extract academy ID from route

            if (requirement == null || expectedAcademyId == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var user = context.User;
            var userAcademyId = user.FindFirstValue(requirement.AcademyIdClaimType);

            if (expectedAcademyId == userAcademyId)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
