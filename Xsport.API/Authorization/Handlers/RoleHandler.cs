using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Xsport.API.Authorization.Requirements;
using Xsport.Common.Constants;
using Xsport.DB.Entities;

namespace Xsport.API.Authorization.Handlers
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var user = context.User;
            if (user == null || requirement == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            if (user.IsInRole(requirement.Role))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
