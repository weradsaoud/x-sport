using System.Security.Claims;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;

namespace Xsport.API.Authentication;

class AuthenticationHandler : IAuthenticationHandler
{
    private HttpContext httpContext = null!;
    private AuthenticationScheme? authenticationScheme;

    public async Task<AuthenticateResult> AuthenticateAsync()
    {
        string? idToken = httpContext?.Request.Headers.Authorization;
        if (string.IsNullOrEmpty(idToken)) return await Task.FromResult(AuthenticateResult.NoResult());
        // FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
        // string Uid = decodedToken.Uid;
        //get user + roles
        Claim c1 = new(ClaimTypes.Name, idToken);
        ClaimsIdentity idten1 = new ClaimsIdentity("UserName");
        idten1.AddClaim(c1);
        ClaimsPrincipal p = new ClaimsPrincipal(idten1);
        AuthenticationTicket ticket = new(p, authenticationScheme?.Name ?? string.Empty);
        return await Task.FromResult(AuthenticateResult.Success(ticket));
    }

    public Task ChallengeAsync(AuthenticationProperties? properties)
    {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    }

    public Task ForbidAsync(AuthenticationProperties? properties)
    {
        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    }

    public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
    {
        httpContext = context;
        authenticationScheme = scheme;
        return Task.CompletedTask;
    }
}
